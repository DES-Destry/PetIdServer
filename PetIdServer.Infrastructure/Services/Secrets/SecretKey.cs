namespace PetIdServer.Infrastructure.Services.Secrets;

public sealed record SecretKey(string Key)
{
    public static readonly SecretKey JwtAccessTokenSecret = new("JwtAccessTokenSecret");
}
