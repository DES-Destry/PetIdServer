using PetIdServer.Application.User.Dto.Tokens;

namespace PetIdServer.Application.User.Commands.Login;

public class LoginResponseDto : TokenPairDto
{
    public Guid UserId { get; set; }
}
