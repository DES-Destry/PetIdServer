using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
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
                    {OnAuthenticationFailed = HandleAuthErrorAsync};
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
                    {OnAuthenticationFailed = HandleAuthErrorAsync};
            });
    }

    private static async Task HandleAuthErrorAsync(AuthenticationFailedContext context)
    {
        var factory =
            context.HttpContext.RequestServices.GetService<ProblemDetailsFactory>();

        var problemDetails = factory == null
            ? new ProblemDetails()
            : factory.CreateProblemDetails(context.HttpContext);

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
