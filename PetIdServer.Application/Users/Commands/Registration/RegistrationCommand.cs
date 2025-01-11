using MediatR;
using PetIdServer.Application.Users.Dto.Tokens;

namespace PetIdServer.Application.Users.Commands.Registration;

public class RegistrationCommand : IRequest<TokenPairDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Name { get; init; }
}
