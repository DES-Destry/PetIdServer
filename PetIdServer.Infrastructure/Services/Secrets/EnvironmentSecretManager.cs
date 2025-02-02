using PetIdServer.Application.Common.Services;

namespace PetIdServer.Infrastructure.Services.Secrets;

public sealed class EnvironmentSecretManager : ISecretManager<SecretKey>
{
    public async Task<string> GetSecretAsync(SecretKey name) => await Task.Factory.StartNew(() => GetSecret(name));

    public async Task<string?> GetSecretOrDefaultAsync(SecretKey name) =>
        await Task.Factory.StartNew(() => GetSecretOrDefault(name));

    private static string GetSecret(SecretKey name)
    {
        string? envValue = Environment.GetEnvironmentVariable(name.Key);
        ArgumentException.ThrowIfNullOrEmpty(envValue);

        return envValue;
    }

    private static string? GetSecretOrDefault(SecretKey name) => Environment.GetEnvironmentVariable(name.Key);
}
