using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.Core.Common.Exceptions.Auth;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Infrastructure.Configuration;

namespace PetIdServer.Infrastructure.Services;

public class AdminTokenService : IAdminTokenService
{
    private readonly AdminTokenParameters _parameters;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly TokenValidationParameters _tokenValidation;

    public AdminTokenService(IConfiguration configuration)
    {
        _tokenHandler = new JwtSecurityTokenHandler();
        _parameters = new AdminTokenParameters(configuration);

        _tokenValidation = new TokenValidationParameters
        {
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_parameters.JwtSecret)),
            ValidAudience = _parameters.Audience,
            ValidIssuer = _parameters.Issuer,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true
        };
    }

    public async Task<string> GenerateToken(Admin admin)
    {
        var adminString = JsonSerializer.Serialize(admin) ??
                          throw new ArgumentException(nameof(admin));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, admin.Username),
            new(ClaimTypes.UserData, adminString)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_parameters.JwtSecret));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_parameters.JwtTtl)),
            Audience = _parameters.Audience,
            Issuer = _parameters.Issuer,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return await Task.FromResult(_tokenHandler.WriteToken(token));
    }

    public async Task<AdminDto> DecryptAdmin(string token)
    {
        await ValidateToken(token);

        var jwtToken = _tokenHandler.ReadJwtToken(token);
        var adminJson = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;

        return JsonSerializer.Deserialize<AdminDto>(adminJson) ??
               throw new ArgumentException(nameof(adminJson));
    }

    private async Task ValidateToken(string token)
    {
        try
        {
            await _tokenHandler.ValidateTokenAsync(token, _tokenValidation);
        }
        catch (SecurityTokenException)
        {
            throw new AccessTokenMalformedException("Access token is not valid!",
                new {Class = nameof(AdminTokenService)});
        }
    }
}
