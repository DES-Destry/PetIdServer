using MediatR;
using PetIdServer.Application.Users.Dto.Tokens;

namespace PetIdServer.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<TokenPairDto>
{
    public required Guid RequesterId { get; init; }
    public string? OldPassword { get; init; }
    public required string NewPassword { get; init; }
}
