namespace PetIdServer.Infrastructure.Configuration.Providers;

public sealed record SecretsProvider(string Value)
{
    public static SecretsProvider Local => new("Local");
    public static SecretsProvider AWS => new("AWS");

    public static implicit operator string(SecretsProvider secretsProvider) => secretsProvider.Value;
}
