using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PetIdServer.Core.Domain.Admin.Exceptions;
using PetIdServer.Core.Domain.Owner.Exceptions;
using PetIdServer.Infrastructure.Configuration;
using PetIdServer.RestApi.Auth;

namespace PetIdServer.RestApi.Extensions;

public static class AuthExtensions
{
    public static AuthenticationBuilder AddPetIdAuthSchemas(
        this AuthenticationBuilder authBuilder,
        IConfiguration configuration)
    {
        var ownerTokenParameters = new OwnerTokenParameters(configuration);
        var adminTokenParameters = new AdminTokenParameters(configuration);

        return authBuilder.AddJwtBearer(AuthSchemas.Owner, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = ownerTokenParameters.Issuer,
                    ValidAudience = ownerTokenParameters.Audience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(ownerTokenParameters.AtSecret))
                };

                options.Events = new JwtBearerEvents
                { OnAuthenticationFailed = _ => throw new OwnerUnauthenticatedException() };
            })
            .AddJwtBearer(AuthSchemas.Admin, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = adminTokenParameters.Issuer,
                    ValidAudience = adminTokenParameters.Audience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(adminTokenParameters.JwtSecret))
                };

                options.Events = new JwtBearerEvents
                { OnAuthenticationFailed = _ => throw new AdminUnauthenticatedException() };
            });
    }
}
