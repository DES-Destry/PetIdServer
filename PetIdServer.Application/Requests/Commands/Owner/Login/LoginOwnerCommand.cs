using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Owner.Login;

public class LoginOwnerCommand : IRequest<TokenPairDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}