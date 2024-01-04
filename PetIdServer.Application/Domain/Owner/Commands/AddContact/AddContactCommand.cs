using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.Owner.Commands.AddContact;

public class AddContactCommand : IRequest<VoidResponseDto>
{
    public string OwnerId { get; set; }
    public string ContactType { get; set; }
    public string Contact { get; set; }
}
