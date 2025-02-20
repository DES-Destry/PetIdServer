using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Pets;
using PetIdServer.Application.TagReports;
using PetIdServer.Application.Tags;
using PetIdServer.Application.Tags.Services;
using PetIdServer.Application.Users;
using PetIdServer.Application.Users.Services;
using PetIdServer.Infrastructure.Configuration;
using PetIdServer.Infrastructure.Configuration.Providers;
using PetIdServer.Infrastructure.Configuration.Providers.Amazon;
using PetIdServer.Infrastructure.Exceptions;
using PetIdServer.Infrastructure.Services;
using PetIdServer.Infrastructure.Services.Secrets;
using PetIdServer.Persistence;
using PetIdServer.Persistence.Repositories;

namespace PetIdServer.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        // Add infrastructure
        services.AddRepositories()
            .AddInfrastructureServices();

        // Add application
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly);
        });

        builder.AddDbConnection();

        string secretsProvider = configuration.GetValue<string>(SecretsConfig.SecretsProvider) ??
                                 throw new MisconfigurationException().WithMeta(new
                                 {
                                     configuration,
                                     value = SecretsConfig.SecretsProvider,
                                     @class = nameof(ServiceCollectionExtension)
                                 });


        if (secretsProvider == SecretsProvider.AWS)
        {
            string region = configuration.GetValue<string>(AmazonSecretsConfig.AwsRegion) ??
                            throw new MisconfigurationException().WithMeta(new
                            {
                                configuration, value = AmazonSecretsConfig.AwsRegion, @class = nameof(ServiceCollectionExtension)
                            });
            string secretName = configuration.GetValue<string>("Secrets:AwsSecretsManagerSecretName") ??
                                throw new MisconfigurationException().WithMeta(new
                                {
                                    configuration,
                                    value = AmazonSecretsConfig.AwsRegion,
                                    @class = nameof(ServiceCollectionExtension)
                                });

            builder.Configuration.AddAmazonSecretsManager(new AmazonSecretsManagerOptions(region, secretName));
        }

        builder.MapConfigurations();

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
        builder.AddNpgsqlDataSource("PetIdPostgresDb");
        builder.AddNpgsqlDbContext<PetIdContext>("PetIdPostgresDb");

        return builder;
    }

    private static void MapConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtTokensParameters>(builder.Configuration);
    }
}
