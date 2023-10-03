using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetIdServer.Application.Exceptions;
using PetIdServer.Application.Services;
using PetIdServer.Core.Entities;
using PetIdServer.Core.Exceptions.Auth;

namespace PetIdServer.Infrastructure.Services;

public class AdminTokenService : IAdminTokenService
{
    private readonly TokenValidationParameters _tokenValidation;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    private readonly string _jwtSecret;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _jwtTtl;

    public AdminTokenService(IConfiguration configuration)
    {
        _tokenHandler = new JwtSecurityTokenHandler();

        _jwtSecret = configuration["Jwt:Admin:Secret"] ??
                     throw new MisconfigurationException().WithMeta(new
                     {
                         configuration,
                         value = "Jwt:Admin:Secret",
                         @class = nameof(AdminTokenService),
                     });
        _issuer = configuration["Jwt:Issuer"] ??
                  throw new MisconfigurationException().WithMeta(new
                  {
                      configuration,
                      value = "Jwt:Issuer",
                      @class = nameof(AdminTokenService),
                  });
        _audience = configuration["Jwt:Audience"] ??
                    throw new MisconfigurationException().WithMeta(new
                    {
                        configuration,
                        value = "Jwt:Audience",
                        @class = nameof(AdminTokenService),
                    });
        _jwtTtl = configuration["Jwt:Admin:Ttl"] ??
                  throw new MisconfigurationException().WithMeta(new
                  {
                      configuration,
                      value = "Jwt:Admin:Ttl",
                      @class = nameof(AdminTokenService),
                  });

        _tokenValidation = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret)),
            ValidAudience = _audience,
            ValidIssuer = _issuer,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true
        };
    }

    public async Task<string> GenerateToken(Admin admin)
    {
        var adminString = JsonSerializer.Serialize(admin) ?? throw new ArgumentException(nameof(admin));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, admin.Username),
            new(ClaimTypes.UserData, adminString)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_jwtTtl)),
            Audience = _audience,
            Issuer = _issuer,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512),
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return await Task.FromResult(_tokenHandler.WriteToken(token));
    }

    public async Task<Admin> DecryptAdmin(string token)
    {
        await ValidateToken(token);

        var jwtToken = _tokenHandler.ReadJwtToken(token);
        var adminJson = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;

        return JsonSerializer.Deserialize<Admin>(adminJson) ?? throw new ArgumentException(nameof(adminJson));
    }

    private async Task ValidateToken(string token)
    {
        try
        {
            var validatedToken = await _tokenHandler.ValidateTokenAsync(token, _tokenValidation);
        }
        catch (SecurityTokenException)
        {
            throw new AccessTokenMalformedException("Access token is not valid!",
                new {Class = nameof(AdminTokenService)});
        }
    }
}