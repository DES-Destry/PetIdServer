using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;

public class RegistrationOwnerCommand : IRequest<TokenPairDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}
