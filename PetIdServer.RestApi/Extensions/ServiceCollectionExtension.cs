using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetIdServer.Infrastructure.Configuration;
using PetIdServer.RestApi.Auth;

namespace PetIdServer.RestApi.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "PetID API - V1",
                Version = "v1",
                Description = "PetID Server.",
            });

            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Description = "JWT Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            // c.AddSecurityDefinition("apiKeyAuth", new OpenApiSecurityScheme()
            // {
            //     Name = "Api Key",
            //     In = ParameterLocation.Header,
            //     Type = SecuritySchemeType.ApiKey,
            //     Description = "Authorization by ApiKey inside request's header",
            //     Scheme = "ApiKeyScheme"
            // });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                // {
                //     new OpenApiSecurityScheme
                //     {
                //         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "apiKeyAuth" }
                //     },
                //     new[] { "SwaggerAuthScheme" }
                // },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                    },
                    new[] {"SwaggerAuthScheme"}
                },
            });
        });

        return services;
    }

    public static AuthenticationBuilder AddPetIdAuthSchemas(
        this AuthenticationBuilder authBuilder,
        OwnerTokenParameters ownerTokenParameters,
        AdminTokenParameters adminTokenParameters)
    {
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ownerTokenParameters.AtSecret))
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
                    ValidIssuer = adminTokenParameters.Issuer,
                    ValidAudience = adminTokenParameters.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(adminTokenParameters.JwtSecret))
                };
            });
    }

    public static IServiceCollection AddPetIdAuthPolicies(this IServiceCollection services)
    {
        return services.AddAuthorization(options =>
        {
            var ownerPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(AuthSchemas.Owner)
                .Build();

            var adminPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(AuthSchemas.Admin)
                .Build();

            options.AddPolicy(AuthSchemas.Owner, ownerPolicy);
            options.AddPolicy(AuthSchemas.Admin, adminPolicy);
        });
    }
}