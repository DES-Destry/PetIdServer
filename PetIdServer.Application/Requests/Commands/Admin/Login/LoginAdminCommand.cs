using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Admin.Login;

public class LoginAdminCommand : IRequest<SingleTokenDto>
{
    public string Username { get; set; }
    public string Password { get; set; }
}