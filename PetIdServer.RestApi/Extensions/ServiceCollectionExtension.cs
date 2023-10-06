using Microsoft.OpenApi.Models;

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
}