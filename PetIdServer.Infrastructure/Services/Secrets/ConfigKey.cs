namespace PetIdServer.Infrastructure.Services.Secrets;

public sealed record ConfigKey(string Key)
{
    public static readonly ConfigKey SecretsProvider = new("Secrets:Provider");
    public static readonly ConfigKey SecretsMappings = new("Secrets:Mappings");

    public static implicit operator string(ConfigKey configKey) => configKey.Key;
}
