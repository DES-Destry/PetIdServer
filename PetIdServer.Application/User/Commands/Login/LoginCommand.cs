using MediatR;
using PetIdServer.Core.Domain.User;

namespace PetIdServer.Application.User.Commands.Login;

public class LoginCommand : IRequest<LoginResponseDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }

    public UserRole? WithPermissionsOf { get; init; }
}
