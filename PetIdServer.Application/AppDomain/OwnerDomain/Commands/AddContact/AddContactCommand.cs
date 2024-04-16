using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.AddContact;

public class AddContactCommand : IRequest<VoidResponseDto>
{
    public Guid OwnerId { get; set; }
    public string ContactType { get; set; }
    public string Contact { get; set; }
}
