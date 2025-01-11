using MediatR;
using PetIdServer.Application.User.Dto.Tokens;

namespace PetIdServer.Application.User.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<TokenPairDto>
{
    public Guid Id { get; init; }
    public string? OldPassword { get; init; }
    public required string NewPassword { get; init; }
}
