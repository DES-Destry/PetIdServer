using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Exceptions;
using PetIdServer.Application.Services;
using PetIdServer.Core.Entities;
using PetIdServer.Core.Exceptions.Auth;

namespace PetIdServer.Infrastructure.Services;

public class OwnerTokenService : IOwnerTokenService
{
    private readonly TokenValidationParameters _tokenValidation;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    private readonly string _atSecret;
    private readonly string _rtSecret;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _atTtl;
    private readonly string _rtTtl;

    public OwnerTokenService(IConfiguration configuration)
    {
        _tokenHandler = new JwtSecurityTokenHandler();

        _atSecret = configuration["Jwt:Owner:Access:Secret"] ??
                    throw new MisconfigurationException().WithMeta(new
                    {
                        configuration,
                        value = "Jwt:Owner:Access:Secret",
                        @class = nameof(OwnerTokenService),
                    });;
        _rtSecret = configuration["Jwt:Owner:Refresh:Secret"] ??
                    throw new MisconfigurationException().WithMeta(new
                    {
                        configuration,
                        value = "Jwt:Owner:Refresh:Secret",
                        @class = nameof(OwnerTokenService),
                    });;
        _issuer = configuration["Jwt:Issuer"] ?? 
                  throw new MisconfigurationException().WithMeta(new
                  {
                      configuration,
                      value = "Jwt:Issuer",
                      @class = nameof(OwnerTokenService),
                  });;
        _audience = configuration["Jwt:Audience"] ??
                    throw new MisconfigurationException().WithMeta(new
                    {
                        configuration,
                        value = "Jwt:Audience",
                        @class = nameof(OwnerTokenService),
                    });;
        _atTtl = configuration["Jwt:Owner:Access:Ttl"] ?? 
                 throw new MisconfigurationException().WithMeta(new
                 {
                     configuration,
                     value = "Jwt:Owner:Access:Ttl",
                     @class = nameof(OwnerTokenService),
                 });;
        _rtTtl = configuration["Jwt:Owner:Refresh:Ttl"] ?? 
                 throw new MisconfigurationException().WithMeta(new
                 {
                     configuration,
                     value = "Jwt:Owner:Refresh:Ttl",
                     @class = nameof(OwnerTokenService),
                 });;

        _tokenValidation = new TokenValidationParameters
        {
            ValidAudience = _audience,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true
        };
    }

    public async Task<TokenPairDto> GenerateTokens(Owner owner)
    {
        var accessToken = GenerateAccessToken(owner);
        var refreshToken = GenerateRefreshToken(accessToken);

        var tokenPair = new TokenPairDto {AccessToken = accessToken, RefreshToken = refreshToken};
        return await Task.FromResult(tokenPair);
    }

    public async Task<TokenPairDto> RefreshTokens(string refreshToken)
    {
        var accessToken = await DecryptAccessToken(refreshToken);
        var owner = await DecryptExpiredOwner(accessToken);

        return await GenerateTokens(owner);
    }

    public async Task<Owner> DecryptOwner(string accessToken)
    {
        await ValidateAccessToken(accessToken);

        var jwtToken = _tokenHandler.ReadJwtToken(accessToken);
        var ownerJson = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;

        return JsonSerializer.Deserialize<Owner>(ownerJson) ?? throw new ArgumentException(nameof(ownerJson));
    }

    private string GenerateAccessToken(Owner owner)
    {
        var ownerString = JsonSerializer.Serialize(owner) ?? throw new ArgumentException(nameof(owner));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, owner.Email),
            new(ClaimTypes.UserData, ownerString)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_atSecret));

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Parse(_atTtl),
            Audience = _audience,
            Issuer = _issuer,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512),
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken(string accessToken)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Hash, accessToken),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_rtSecret));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Parse(_rtTtl),
            Audience = _audience,
            Issuer = _issuer,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512),
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    private async Task<Owner> DecryptExpiredOwner(string accessToken)
    {
        await ValidateExpiredAccessToken(accessToken);

        var jwtToken = _tokenHandler.ReadJwtToken(accessToken);
        var ownerJson = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;

        return JsonSerializer.Deserialize<Owner>(ownerJson) ?? throw new ArgumentException(nameof(ownerJson));
    }

    private async Task<string> DecryptAccessToken(string refreshToken)
    {
        await ValidateRefreshToken(refreshToken);

        var jwtToken = _tokenHandler.ReadJwtToken(refreshToken);
        return jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Hash).Value;
    }

    private async Task ValidateAccessToken(string accessToken)
    {
        _tokenValidation.IssuerSigningKey = new JsonWebKey(_atSecret);
        _tokenValidation.ValidateLifetime = true;

        var validated = await ValidateTokens(accessToken);

        if (!validated)
            throw new AccessTokenMalformedException("Access token is not valid!",
                new {Class = nameof(OwnerTokenService)});
    }

    private async Task ValidateExpiredAccessToken(string accessToken)
    {
        _tokenValidation.IssuerSigningKey = new JsonWebKey(_atSecret);
        _tokenValidation.ValidateLifetime = false;

        var validated = await ValidateTokens(accessToken);

        if (!validated)
            throw new AccessTokenMalformedException("Access token is not valid!",
                new {Class = nameof(OwnerTokenService)});
    }

    private async Task ValidateRefreshToken(string refreshToken)
    {
        _tokenValidation.IssuerSigningKey = new JsonWebKey(_rtSecret);
        _tokenValidation.ValidateLifetime = true;

        var validated = await ValidateTokens(refreshToken);

        if (!validated)
            throw new RefreshTokenMalformedException("Refresh token is not valid!",
                new {Class = nameof(OwnerTokenService)});
    }

    private async Task<bool> ValidateTokens(string token)
    {
        try
        {
            var validatedToken = await _tokenHandler.ValidateTokenAsync(token, _tokenValidation);
            return validatedToken != null;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
    }
}