using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetIdServer.Application.AppDomain.AdminDomain;
using PetIdServer.Application.AppDomain.OwnerDomain;
using PetIdServer.Application.AppDomain.PetDomain;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Application.Common.Exceptions;
using PetIdServer.Application.Common.Services;
using PetIdServer.Infrastructure.Database;
using PetIdServer.Infrastructure.Database.Repositories;
using PetIdServer.Infrastructure.Mapper;
using PetIdServer.Infrastructure.Services;

namespace PetIdServer.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("Postgres") ??
                               throw new MisconfigurationException().WithMeta(new
                               {
                                   configuration,
                                   value = "ConnectionStrings:Postgres",
                                   @class = "Infrastructure.Extensions"
                               });

        services.AddAutoMapper(typeof(InfrastructureMappingProfile));

        services.AddDbContext<PetIdContext>(options =>
        {
            options.UseNpgsql(connectionString);

            if (environment.IsProduction()) return;

            // Debug options section
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        services.AddRepositories();
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IOwnerTokenService, OwnerTokenService>();
        services.AddScoped<IAdminTokenService, AdminTokenService>();
        services.AddScoped<ICodeDecoder, CodeDecoder>();

        return services;
    }
}
