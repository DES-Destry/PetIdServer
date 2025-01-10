using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.User.Commands.Login;

public class LoginResponseDto : TokenPairDto
{
    public Guid UserId { get; set; }
}
