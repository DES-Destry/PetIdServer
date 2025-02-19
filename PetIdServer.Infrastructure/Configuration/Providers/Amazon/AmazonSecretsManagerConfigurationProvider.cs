using System.Text;
using System.Text.Json;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using PetIdServer.Infrastructure.Exceptions;

namespace PetIdServer.Infrastructure.Configuration.Providers.Amazon;

public class AmazonSecretsManagerConfigurationProvider(string region, string secretName) : ConfigurationProvider
{
    public override void Load()
    {
        string secretsJson = DownloadSecretsJsonFromAws();

        Data = JsonSerializer.Deserialize<Dictionary<string, string?>>(secretsJson) ??
               throw new AmazonSecretsInvalidJsonException();
    }

    private string DownloadSecretsJsonFromAws()
    {
        GetSecretValueRequest request = new()
        {
            SecretId = secretName
        };

        using AmazonSecretsManagerClient awsSecretsClient = new(RegionEndpoint.GetBySystemName(region));
        GetSecretValueResponse response = awsSecretsClient.GetSecretValueAsync(request).GetAwaiter().GetResult();

        if (response.SecretString is not null)
        {
            return response.SecretString;
        }

        MemoryStream bin = response.SecretBinary;
        using StreamReader reader = new(bin);

        return Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
    }
}
