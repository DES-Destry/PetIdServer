using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.User.Commands.RemoveContact;

public class RemoveContactCommand : IRequest<VoidResponseDto>
{
    public required Guid UserId { get; set; }
    public required string ContactType { get; set; }
}
