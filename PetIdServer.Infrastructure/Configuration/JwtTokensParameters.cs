namespace PetIdServer.Infrastructure.Configuration;

public class JwtTokensParameters
{
    public required string JwtAccessTokenSecret { get; set; }
    public required string JwtAccessTokenTtl { get; set; }
    public required string JwtAccessTokenTtlManager { get; set; }
    public required string JwtRefreshTokenSecret { get; set; }
    public required string JwtRefreshTokenTtl { get; set; }
    public required string JwtRefreshTokenTtlManager { get; set; }
    public required string JwtIssuer { get; set; }
    public required string JwtAudience { get; set; }
}
