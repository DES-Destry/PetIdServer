namespace PetIdServer.Infrastructure.Configuration.Providers.Amazon;

public record AmazonSecretsManagerOptions(string Region, string SecretName);
