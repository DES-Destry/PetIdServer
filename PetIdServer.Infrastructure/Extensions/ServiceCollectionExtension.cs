using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Pet;
using PetIdServer.Application.Tag;
using PetIdServer.Application.Tag.Services;
using PetIdServer.Application.TagReport;
using PetIdServer.Application.User;
using PetIdServer.Application.User.Services;
using PetIdServer.Infrastructure.Database;
using PetIdServer.Infrastructure.Database.Repositories;
using PetIdServer.Infrastructure.Services;

namespace PetIdServer.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        // Add infrastructure
        services.AddAutoMapper(AssemblyReference.Assembly)
            .AddRepositories()
            .AddInfrastructureServices();

        // Add application
        services
            .AddAutoMapper(Application.AssemblyReference.Assembly)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly);
            });

        builder.AddDbConnection();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ITagReportRepository, TagReportRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IUserTokenService, UserTokenService>();
        services.AddScoped<ICodeDecoder, CodeDecoder>();

        return services;
    }

    private static IHostApplicationBuilder AddDbConnection(
        this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDataSource("pet-id");
        builder.AddNpgsqlDbContext<PetIdContext>("pet-id");

        return builder;
    }
}
