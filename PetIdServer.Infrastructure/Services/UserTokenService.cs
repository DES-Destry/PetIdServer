using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.Core.User.Exceptions.Auth;
using PetIdServer.Infrastructure.Configuration;

namespace PetIdServer.Infrastructure.Services;

public enum TokenType { Access, Refresh }

public class UserTokenService : IUserTokenService
{
    private readonly TokenValidationParameters _accessTokenSoftValidationParameters;
    private readonly TokenValidationParameters _accessTokenValidationParameters;
    private readonly JwtTokensParameters _parameters;
    private readonly TokenValidationParameters _refreshTokenValidationParameters;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public UserTokenService(IConfiguration configuration)
    {
        _tokenHandler = new JwtSecurityTokenHandler();
        _parameters = new JwtTokensParameters(configuration);

        _accessTokenValidationParameters = CreateTokenValidationParameters(TokenType.Access, true);
        _accessTokenSoftValidationParameters = CreateTokenValidationParameters(TokenType.Access, false);
        _refreshTokenValidationParameters = CreateTokenValidationParameters(TokenType.Refresh, true);
    }

    public Task<TokenPairDto> GenerateTokens(UserDto user)
    {
        string accessToken = GenerateAccessToken(user);
        string refreshToken = GenerateRefreshToken(accessToken);
        return Task.FromResult(new TokenPairDto
        {
            AccessToken = accessToken, RefreshToken = refreshToken
        });
    }

    public async Task<TokenPairDto> RefreshTokens(string refreshToken)
    {
        string accessToken = await GetAccessTokenFromRefreshToken(refreshToken);
        UserDto user = await GetUserFromTokenEvenIfExpired(accessToken);
        return await GenerateTokens(user);
    }

    public async Task<UserDto> GetUserFromToken(string accessToken)
    {
        await ValidateAccessToken(accessToken);
        return DeserializeUserFromToken(accessToken);
    }

    // -------------------------------------------------
    // Generate tokens
    // -------------------------------------------------

    private SecurityTokenDescriptor GetTokenParametersForGenerating(List<Claim> claims, TokenType tokenType)
    {
        string secret = tokenType switch
        {
            TokenType.Access => _parameters.AtSecret,
            TokenType.Refresh => _parameters.RtSecret,
            _ => throw new ArgumentException("There's access and refresh tokens only!", nameof(tokenType))
        };

        string ttl = tokenType switch
        {
            TokenType.Access => _parameters.PetOwnerAtTtl,
            TokenType.Refresh => _parameters.PetOwnerRtTtl,
            _ => throw new ArgumentException("There's access and refresh tokens only!", nameof(tokenType))
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secret));

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(ttl)),
            Audience = _parameters.Audience,
            Issuer = _parameters.Issuer,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
        };

        return tokenDescriptor;
    }

    private string GenerateAccessToken(UserDto user)
    {
        string userJson = JsonSerializer.Serialize(user) ??
                          throw new ArgumentException("Cannot make JSON from object", nameof(user));

        List<Claim> claims = [new(ClaimTypes.Email, user.Email), new(ClaimTypes.UserData, userJson)];
        SecurityTokenDescriptor tokenDescriptor = GetTokenParametersForGenerating(claims, TokenType.Access);

        SecurityToken? token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken(string accessToken)
    {
        List<Claim> claims = [new(ClaimTypes.Hash, accessToken)];
        SecurityTokenDescriptor tokenDescriptor = GetTokenParametersForGenerating(claims, TokenType.Refresh);

        SecurityToken? token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    // -------------------------------------------------
    // Get information from tokens
    // -------------------------------------------------

    private static string GetPayloadFromToken(JwtSecurityToken jwtSecurityToken, TokenType tokenType) => tokenType switch
    {
        TokenType.Access => jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value,
        TokenType.Refresh => jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Hash).Value,
        _ => throw new ArgumentException("There's access and refresh tokens only!", nameof(tokenType))
    };

    private UserDto DeserializeUserFromToken(string accessToken)
    {
        JwtSecurityToken jwtToken = _tokenHandler.ReadJwtToken(accessToken) ??
                                    throw new ArgumentException("Access token invalid format", nameof(accessToken));
        string userJson = GetPayloadFromToken(jwtToken, TokenType.Access);
        return JsonSerializer.Deserialize<UserDto>(userJson) ??
               throw new ArgumentException(nameof(userJson));
    }

    private async Task<UserDto> GetUserFromTokenEvenIfExpired(string accessToken)
    {
        await ValidateAccessTokenEvenIfExpired(accessToken);
        return DeserializeUserFromToken(accessToken);
    }

    private async Task<string> GetAccessTokenFromRefreshToken(string refreshToken)
    {
        await ValidateRefreshToken(refreshToken);

        JwtSecurityToken jwtToken = _tokenHandler.ReadJwtToken(refreshToken) ??
                                    throw new ArgumentException("Refresh token invalid format", nameof(refreshToken));
        return GetPayloadFromToken(jwtToken, TokenType.Refresh);
    }

    // -------------------------------------------------
    // Token validations
    // -------------------------------------------------

    private TokenValidationParameters CreateTokenValidationParameters(TokenType tokenType,
        bool validateLifetime)
    {
        string secret = tokenType switch
        {
            TokenType.Access => _parameters.AtSecret,
            TokenType.Refresh => _parameters.RtSecret,
            _ => throw new ArgumentException("There's access and refresh tokens only!", nameof(tokenType))
        };

        return new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidAudience = _parameters.Audience,
            ValidIssuer = _parameters.Issuer,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = validateLifetime
        };
    }

    private async Task ValidateAccessToken(string accessToken)
    {
        bool validated = await ValidateTokens(accessToken, _accessTokenValidationParameters);

        if (!validated)
        {
            throw new AccessTokenMalformedException("Access token is not valid!",
                                                    new
                                                    {
                                                        Class = nameof(UserTokenService)
                                                    });
        }
    }

    private async Task ValidateAccessTokenEvenIfExpired(string accessToken)
    {
        bool validated = await ValidateTokens(accessToken, _accessTokenSoftValidationParameters);

        if (!validated)
        {
            throw new AccessTokenMalformedException("Access token is not valid!",
                                                    new
                                                    {
                                                        Class = nameof(UserTokenService)
                                                    });
        }
    }

    private async Task ValidateRefreshToken(string refreshToken)
    {
        bool validated = await ValidateTokens(refreshToken, _refreshTokenValidationParameters);

        if (!validated)
        {
            throw new RefreshTokenMalformedException("Refresh token is not valid!",
                                                     new
                                                     {
                                                         Class = nameof(UserTokenService)
                                                     });
        }
    }

    private async Task<bool> ValidateTokens(string token, TokenValidationParameters validationParameters)
    {
        try
        {
            TokenValidationResult? validatedToken =
                await _tokenHandler.ValidateTokenAsync(token, validationParameters);
            return validatedToken != null;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
    }
}
