using PetIdServer.Application.Common.Services;

namespace PetIdServer.Infrastructure.Services.Secrets;

public sealed class EnvironmentSecretManager(SecretKeyFetcher secretKeyFetcher) : ISecretManager<ConfigKeyForSecret>
{
    public async Task<string> GetSecretAsync(ConfigKeyForSecret configKeyForSecret) =>
        await Task.Factory.StartNew(() => GetSecret(configKeyForSecret));

    public async Task<string?> GetSecretOrDefaultAsync(ConfigKeyForSecret configKeyForSecret) =>
        await Task.Factory.StartNew(() => GetSecretOrDefault(configKeyForSecret));

    private string GetSecret(ConfigKeyForSecret configKeyFor)
    {
        string envKey = secretKeyFetcher.GetKeyValue(configKeyFor);
        string? envValue = Environment.GetEnvironmentVariable(envKey);
        ArgumentException.ThrowIfNullOrEmpty(envValue);

        return envValue;
    }

    private string? GetSecretOrDefault(ConfigKeyForSecret configKeyFor)
    {
        string envKey = secretKeyFetcher.GetKeyValue(configKeyFor);
        return Environment.GetEnvironmentVariable(envKey);
    }
}
