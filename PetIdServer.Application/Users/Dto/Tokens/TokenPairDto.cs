namespace PetIdServer.Application.Users.Dto.Tokens;

public class TokenPairDto
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}
