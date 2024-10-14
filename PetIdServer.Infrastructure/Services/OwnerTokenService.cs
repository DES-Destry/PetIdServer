using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.Core.Common.Exceptions.Auth;
using PetIdServer.Infrastructure.Configuration;

namespace PetIdServer.Infrastructure.Services;

public enum OwnerTokenType { Access, Refresh }

public class OwnerTokenService : IOwnerTokenService
{
    private readonly TokenValidationParameters _accessTokenSoftValidationParameters;
    private readonly TokenValidationParameters _accessTokenValidationParameters;
    private readonly OwnerTokensParameters _parameters;
    private readonly TokenValidationParameters _refreshTokenValidationParameters;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public OwnerTokenService(IConfiguration configuration)
    {
        _tokenHandler = new JwtSecurityTokenHandler();
        _parameters = new OwnerTokensParameters(configuration);

        _accessTokenValidationParameters = CreateTokenValidationParameters(OwnerTokenType.Access, true);
        _accessTokenSoftValidationParameters = CreateTokenValidationParameters(OwnerTokenType.Access, false);
        _refreshTokenValidationParameters = CreateTokenValidationParameters(OwnerTokenType.Refresh, true);
    }

    public Task<TokenPairDto> GenerateTokens(OwnerDto owner)
    {
        string accessToken = GenerateAccessToken(owner);
        string refreshToken = GenerateRefreshToken(accessToken);
        return Task.FromResult(new TokenPairDto
        {
            AccessToken = accessToken, RefreshToken = refreshToken
        });
    }

    public async Task<TokenPairDto> RefreshTokens(string refreshToken)
    {
        string accessToken = await GetAccessTokenFromRefreshToken(refreshToken);
        OwnerDto owner = await GetOwnerFromTokenEvenIfExpired(accessToken);
        return await GenerateTokens(owner);
    }

    public async Task<OwnerDto> GetOwnerFromToken(string accessToken)
    {
        await ValidateAccessToken(accessToken);
        return DeserializeOwnerFromToken(accessToken);
    }

    // -------------------------------------------------
    // Generate tokens
    // -------------------------------------------------

    private SecurityTokenDescriptor GetTokenParametersForGenerating(List<Claim> claims, OwnerTokenType tokenType)
    {
        string secret = tokenType switch
        {
            OwnerTokenType.Access => _parameters.AtSecret,
            OwnerTokenType.Refresh => _parameters.RtSecret,
            _ => throw new ArgumentException("There's access and refresh tokens only!", nameof(tokenType))
        };

        string ttl = tokenType switch
        {
            OwnerTokenType.Access => _parameters.AtTtl,
            OwnerTokenType.Refresh => _parameters.RtTtl,
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

    private string GenerateAccessToken(OwnerDto owner)
    {
        string ownerJson = JsonSerializer.Serialize(owner) ??
                           throw new ArgumentException("Cannot make JSON from object", nameof(owner));

        List<Claim> claims = [new(ClaimTypes.Email, owner.Email), new(ClaimTypes.UserData, ownerJson)];
        SecurityTokenDescriptor tokenDescriptor = GetTokenParametersForGenerating(claims, OwnerTokenType.Access);

        SecurityToken? token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken(string accessToken)
    {
        List<Claim> claims = [new(ClaimTypes.Hash, accessToken)];
        SecurityTokenDescriptor tokenDescriptor = GetTokenParametersForGenerating(claims, OwnerTokenType.Refresh);

        SecurityToken? token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    // -------------------------------------------------
    // Get information from tokens
    // -------------------------------------------------

    private static string GetPayloadFromToken(JwtSecurityToken jwtSecurityToken, OwnerTokenType tokenType) => tokenType switch
    {
        OwnerTokenType.Access => jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value,
        OwnerTokenType.Refresh => jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Hash).Value,
        _ => throw new ArgumentException("There's access and refresh tokens only!", nameof(tokenType))
    };

    private OwnerDto DeserializeOwnerFromToken(string accessToken)
    {
        JwtSecurityToken jwtToken = _tokenHandler.ReadJwtToken(accessToken) ??
                                    throw new ArgumentException("Access token invalid format", nameof(accessToken));
        string ownerJson = GetPayloadFromToken(jwtToken, OwnerTokenType.Access);
        return JsonSerializer.Deserialize<OwnerDto>(ownerJson) ??
               throw new ArgumentException(nameof(ownerJson));
    }

    private async Task<OwnerDto> GetOwnerFromTokenEvenIfExpired(string accessToken)
    {
        await ValidateAccessTokenEvenIfExpired(accessToken);
        return DeserializeOwnerFromToken(accessToken);
    }

    private async Task<string> GetAccessTokenFromRefreshToken(string refreshToken)
    {
        await ValidateRefreshToken(refreshToken);

        JwtSecurityToken jwtToken = _tokenHandler.ReadJwtToken(refreshToken) ??
                                    throw new ArgumentException("Refresh token invalid format", nameof(refreshToken));
        return GetPayloadFromToken(jwtToken, OwnerTokenType.Refresh);
    }

    // -------------------------------------------------
    // Token validations
    // -------------------------------------------------

    private TokenValidationParameters CreateTokenValidationParameters(OwnerTokenType tokenType,
        bool validateLifetime)
    {
        string secret = tokenType switch
        {
            OwnerTokenType.Access => _parameters.AtSecret,
            OwnerTokenType.Refresh => _parameters.RtSecret,
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
                                                        Class = nameof(OwnerTokenService)
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
                                                        Class = nameof(OwnerTokenService)
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
                                                         Class = nameof(OwnerTokenService)
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
