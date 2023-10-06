using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Services;
using PetIdServer.Application.Services.Dto;
using PetIdServer.Core.Exceptions.Auth;
using PetIdServer.Infrastructure.Configuration;

namespace PetIdServer.Infrastructure.Services;

public class OwnerTokenService : IOwnerTokenService
{
    private readonly TokenValidationParameters _tokenValidation;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly OwnerTokenParameters _parameters;

    public OwnerTokenService(IConfiguration configuration)
    {
        _tokenHandler = new JwtSecurityTokenHandler();
        _parameters = new OwnerTokenParameters(configuration);
        
        _tokenValidation = new TokenValidationParameters
        {
            ValidAudience = _parameters.Audience,
            ValidIssuer = _parameters.Issuer,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true
        };
    }

    public async Task<TokenPairDto> GenerateTokens(OwnerDto owner)
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

    public async Task<OwnerDto> DecryptOwner(string accessToken)
    {
        await ValidateAccessToken(accessToken);

        var jwtToken = _tokenHandler.ReadJwtToken(accessToken);
        var ownerJson = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;

        return JsonSerializer.Deserialize<OwnerDto>(ownerJson) ?? throw new ArgumentException(nameof(ownerJson));
    }

    private string GenerateAccessToken(OwnerDto owner)
    {
        var ownerString = JsonSerializer.Serialize(owner) ?? throw new ArgumentException(nameof(owner));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, owner.Email),
            new(ClaimTypes.UserData, ownerString)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_parameters.AtSecret));

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_parameters.AtTtl)),
            Audience = _parameters.Audience,
            Issuer = _parameters.Issuer,
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
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_parameters.RtSecret));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_parameters.RtTtl)),
            Audience = _parameters.Audience,
            Issuer = _parameters.Issuer,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512),
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    private async Task<OwnerDto> DecryptExpiredOwner(string accessToken)
    {
        await ValidateExpiredAccessToken(accessToken);

        var jwtToken = _tokenHandler.ReadJwtToken(accessToken);
        var ownerJson = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;

        return JsonSerializer.Deserialize<OwnerDto>(ownerJson) ?? throw new ArgumentException(nameof(ownerJson));
    }

    private async Task<string> DecryptAccessToken(string refreshToken)
    {
        await ValidateRefreshToken(refreshToken);

        var jwtToken = _tokenHandler.ReadJwtToken(refreshToken);
        return jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Hash).Value;
    }

    private async Task ValidateAccessToken(string accessToken)
    {
        _tokenValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_parameters.AtSecret));
        _tokenValidation.ValidateLifetime = true;

        var validated = await ValidateTokens(accessToken);

        if (!validated)
            throw new AccessTokenMalformedException("Access token is not valid!",
                new {Class = nameof(OwnerTokenService)});
    }

    private async Task ValidateExpiredAccessToken(string accessToken)
    {
        _tokenValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_parameters.AtSecret));
        _tokenValidation.ValidateLifetime = false;

        var validated = await ValidateTokens(accessToken);

        if (!validated)
            throw new AccessTokenMalformedException("Access token is not valid!",
                new {Class = nameof(OwnerTokenService)});
    }

    private async Task ValidateRefreshToken(string refreshToken)
    {
        _tokenValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_parameters.RtSecret));
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