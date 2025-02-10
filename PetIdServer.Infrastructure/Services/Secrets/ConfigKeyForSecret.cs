namespace PetIdServer.Infrastructure.Services.Secrets;

public sealed record ConfigKeyForSecret(string Key)
{
    public static readonly ConfigKeyForSecret JwtAccessTokenConfigKeyForSecret = new("JwtAccessTokenSecret");
    public static readonly ConfigKeyForSecret JwtAccessTokenTtl = new("JwtAccessTokenTtl");
    public static readonly ConfigKeyForSecret JwtAccessTokenTtlForManagers = new("JwtAccessTokenTtlForManagers");

    public static readonly ConfigKeyForSecret JwtRefreshTokenConfigKeyForSecret = new("JwtRefreshTokenSecret");
    public static readonly ConfigKeyForSecret JwtRefreshTokenTtl = new("JwtRefreshTokenTtl");
    public static readonly ConfigKeyForSecret JwtRefreshTokenTtlForManagers = new("JwtRefreshTokenTtlForManagers");

    public static readonly ConfigKeyForSecret JwtIssuer = new("JwtIssuer");
    public static readonly ConfigKeyForSecret JwtAudience = new("JwtAudience");

    public static readonly ConfigKeyForSecret RequestSecurity = new("RequestSecurityKey");
    public static readonly ConfigKeyForSecret DatabaseConnectionString = new("DatabaseConnectionString");
    public string FullKeyPath => $"{ConfigKey.SecretsMappings}:{Key}";

    public static implicit operator string(ConfigKeyForSecret configKeyFor) => configKeyFor.FullKeyPath;
}
