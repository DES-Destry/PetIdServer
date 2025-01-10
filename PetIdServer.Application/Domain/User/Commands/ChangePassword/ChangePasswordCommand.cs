using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.User.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<TokenPairDto>
{
    public Guid Id { get; init; }
    public string? OldPassword { get; init; }
    public required string NewPassword { get; init; }
}
