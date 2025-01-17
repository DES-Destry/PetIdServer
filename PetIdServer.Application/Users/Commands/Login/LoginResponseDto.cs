using PetIdServer.Application.Users.Dto.Tokens;

namespace PetIdServer.Application.Users.Commands.Login;

public class LoginResponseDto : TokenPairDto
{
    public required Guid UserId { get; init; }
}
