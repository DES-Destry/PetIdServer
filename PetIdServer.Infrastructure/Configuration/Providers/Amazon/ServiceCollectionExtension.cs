using Microsoft.Extensions.Configuration;

namespace PetIdServer.Infrastructure.Configuration.Providers.Amazon;

public static class ServiceCollectionExtension
{
    public static void AddAmazonSecretsManager(
        this IConfigurationBuilder configurationBuilder,
        AmazonSecretsManagerOptions options)
    {
        configurationBuilder.Add(new AmazonSecretsManagerConfigurationSource(options.Region, options.SecretName));
    }
}
