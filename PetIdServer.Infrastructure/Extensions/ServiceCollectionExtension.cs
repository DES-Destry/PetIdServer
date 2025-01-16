using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Pets;
using PetIdServer.Application.TagReports;
using PetIdServer.Application.Tags;
using PetIdServer.Application.Tags.Services;
using PetIdServer.Application.Users;
using PetIdServer.Application.Users.Services;
using PetIdServer.Infrastructure.Services;
using PetIdServer.Persistence;
using PetIdServer.Persistence.Repositories;

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
