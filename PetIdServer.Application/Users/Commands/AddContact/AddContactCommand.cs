using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Users.Commands.AddContact;

public class AddContactCommand : IRequest<VoidResponseDto>
{
    public required Guid UserId { get; init; }
    public required string ContactType { get; init; }
    public required string Contact { get; init; }
}
