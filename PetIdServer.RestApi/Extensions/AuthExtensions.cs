using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PetIdServer.Core.Domain.User;
using PetIdServer.Core.Domain.User.Exceptions;
using PetIdServer.Infrastructure.Configuration;
using PetIdServer.RestApi.Auth;

namespace PetIdServer.RestApi.Extensions;

public static class AuthExtensions
{
    public static AuthenticationBuilder AddPetIdAuthSchemas(
        this AuthenticationBuilder authBuilder,
        IConfiguration configuration)
    {
        JwtTokensParameters jwtTokenParameters = new(configuration);

        return authBuilder.AddJwtBearer(AuthSchemas.PetOwner, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtTokenParameters.Issuer,
                    ValidAudience = jwtTokenParameters.Audience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtTokenParameters.AtSecret))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = _ => throw new UserUnauthenticatedException()
                };
            })
            .AddJwtBearer(AuthSchemas.TagChecker, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtTokenParameters.Issuer,
                    ValidAudience = jwtTokenParameters.Audience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtTokenParameters.AtSecret))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ValidateRole(UserRole.TagChecker),
                    OnAuthenticationFailed = _ => throw new UserUnauthenticatedException()
                };
            })
            .AddJwtBearer(AuthSchemas.Admin, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtTokenParameters.Issuer,
                    ValidAudience = jwtTokenParameters.Audience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtTokenParameters.AtSecret))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ValidateRole(UserRole.Admin),
                    OnAuthenticationFailed = _ => throw new UserUnauthenticatedException()
                };
            });
    }

    private static Func<TokenValidatedContext, Task> ValidateRole(UserRole requiredRole) => context =>
    {
        Claim? permissionClaim = context.Principal?.FindFirst("WithPermissionsOf");
        bool tokenHasPermissionsOfRequiredRole =
            UserRole.TryParse(permissionClaim?.Value, out UserRole? permissionRole) &&
            permissionRole.HasPermissionsOf(requiredRole);

        if (!tokenHasPermissionsOfRequiredRole)
        {
            throw new UserUnauthorizedException("User does not have the required permissions",
                                                new
                                                {
                                                    CurrentRole = permissionClaim?.Value ?? UserRole.LeastPrivileged.Name,
                                                    CurrentRoleLevel = permissionRole?.Level ?? UserRole.LeastPrivileged.Level,
                                                    RequiredRoleName = requiredRole.Name,
                                                    RequiredRoleLevel = requiredRole.Level
                                                });
        }

        return Task.CompletedTask;
    };
}
