using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Domain.Pet;
using PetIdServer.Application.Domain.Tag;
using PetIdServer.Application.Domain.TagReport;
using PetIdServer.Application.Domain.User;
using PetIdServer.Infrastructure.Database;
using PetIdServer.Infrastructure.Database.Domain.Pet;
using PetIdServer.Infrastructure.Database.Domain.Tag;
using PetIdServer.Infrastructure.Database.Domain.User;
using PetIdServer.Infrastructure.Mapper;
using PetIdServer.Infrastructure.Services;

namespace PetIdServer.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddAutoMapper(typeof(InfrastructureMappingProfile));
        builder.AddDbConnection();

        services.AddRepositories();
        services.AddInfrastructureServices();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
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

    public static IHostApplicationBuilder AddDbConnection(
        this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDataSource("pet-id");
        builder.AddNpgsqlDbContext<PetIdContext>("pet-id");

        return builder;
    }
}
