using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Owner.Registration;

public class RegistrationOwnerCommand : IRequest<TokenPairDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}