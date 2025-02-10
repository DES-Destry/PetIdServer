namespace PetIdServer.Infrastructure.Services.Secrets;

public sealed record SecretsProvider(string Value)
{
    public static SecretsProvider Environment => new("Environment");
    public static SecretsProvider AWS => new("AWS");

    public static implicit operator string(SecretsProvider secretsProvider) => secretsProvider.Value;
}
