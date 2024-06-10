using MediatR;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;

public class RegistrationOwnerCommand : IRequest<LoginOwnerResponseDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
}
