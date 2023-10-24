using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Owner.RemoveContact;

public class RemoveContactCommand : IRequest<VoidResponseDto>
{
    public string OwnerEmail { get; set; }
    public string ContactType { get; set; }
}