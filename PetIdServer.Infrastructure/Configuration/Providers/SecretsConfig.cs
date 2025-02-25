namespace PetIdServer.Infrastructure.Configuration.Providers;

public record SecretsConfig(string Key)
{
    public static readonly SecretsConfig SecretsProvider = new("Secrets:Provider");

    public static implicit operator string(SecretsConfig configKey) => configKey.Key;
}
