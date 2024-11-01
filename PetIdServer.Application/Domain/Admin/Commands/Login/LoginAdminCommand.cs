using MediatR;

namespace PetIdServer.Application.Domain.Admin.Commands.Login;

public class LoginAdminCommand : IRequest<LoginAdminResponseDto>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
