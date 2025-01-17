using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Users.Commands.RemoveContact;

public class RemoveContactCommand : IRequest<VoidResponseDto>
{
    public required Guid UserId { get; init; }
    public required string ContactType { get; init; }
}
