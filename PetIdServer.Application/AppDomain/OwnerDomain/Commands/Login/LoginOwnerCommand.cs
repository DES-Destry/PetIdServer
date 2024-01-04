using MediatR;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;

public class LoginOwnerCommand : IRequest<LoginOwnerResponseDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
