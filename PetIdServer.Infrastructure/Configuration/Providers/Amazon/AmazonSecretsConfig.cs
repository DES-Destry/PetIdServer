namespace PetIdServer.Infrastructure.Configuration.Providers.Amazon;

public record AmazonSecretsConfig(string Key) : SecretsConfig(Key)
{
    public static readonly AmazonSecretsConfig AwsRegion = new("Secrets:AwsRegion");
    public static readonly AmazonSecretsConfig AwsSecretsManagerSecretName = new("Secrets:AwsSecretsManagerSecretName");
}
