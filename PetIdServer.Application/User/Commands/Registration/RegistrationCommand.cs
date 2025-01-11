using MediatR;
using PetIdServer.Application.User.Dto.Tokens;

namespace PetIdServer.Application.User.Commands.Registration;

public class RegistrationCommand : IRequest<TokenPairDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Name { get; init; }
}
