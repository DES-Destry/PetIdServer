using Microsoft.Extensions.Configuration;

namespace PetIdServer.Infrastructure.Configuration.Providers.Amazon;

public class AmazonSecretsManagerConfigurationSource(string region, string secretName) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new AmazonSecretsManagerConfigurationProvider(region, secretName);
}
