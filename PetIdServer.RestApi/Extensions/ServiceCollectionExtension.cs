using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using PetIdServer.RestApi.Auth;

namespace PetIdServer.RestApi.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PetID API - V1",
                Version = "v1",
                Description = "PetID Server."
            });

            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Description = "JWT Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityDefinition("securityKeyAuth", new OpenApiSecurityScheme
            {
                Name = "X-Security-Key",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "Authorization by SecurityKey inside request's header",
                Scheme = "SecurityKeyScheme"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "securityKeyAuth"}
                    },
                    new[] {"SwaggerAuthScheme"}
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                    },
                    new[] {"SwaggerAuthScheme"}
                }
            });
        });

        return services;
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