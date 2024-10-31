using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetIdServer.Application.AppDomain.AdminDomain;
using PetIdServer.Application.AppDomain.OwnerDomain;
using PetIdServer.Application.AppDomain.PetDomain;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Application.AppDomain.TagReportDomain;
using PetIdServer.Application.Common.Services;
using PetIdServer.Infrastructure.Database;
using PetIdServer.Infrastructure.Database.Domain.Admin;
using PetIdServer.Infrastructure.Database.Domain.Owner;
using PetIdServer.Infrastructure.Database.Domain.Pet;
using PetIdServer.Infrastructure.Database.Domain.Tag;
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
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ITagReportRepository, TagReportRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IOwnerTokenService, OwnerTokenService>();
        services.AddScoped<IAdminTokenService, AdminTokenService>();
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
