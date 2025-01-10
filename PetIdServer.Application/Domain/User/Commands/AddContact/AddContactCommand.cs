using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.User.Commands.AddContact;

public class AddContactCommand : IRequest<VoidResponseDto>
{
    public required Guid UserId { get; set; }
    public required string ContactType { get; set; }
    public required string Contact { get; set; }
}
