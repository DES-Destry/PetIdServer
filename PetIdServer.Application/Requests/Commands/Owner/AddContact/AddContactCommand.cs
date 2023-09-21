using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Owner.AddContact;

public class AddContactCommand : IRequest<VoidResponseDto>
{
    public string OwnerId { get; set; }
    public string ContactType { get; set; }
    public string Contact { get; set; }
}