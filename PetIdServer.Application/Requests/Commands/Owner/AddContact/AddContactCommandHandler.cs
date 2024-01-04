using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Domains.Owner;
using PetIdServer.Core.Domains.Owner.Exceptions;

namespace PetIdServer.Application.Requests.Commands.Owner.AddContact;

public class AddContactCommandHandler(IOwnerRepository ownerRepository)
    : IRequestHandler<AddContactCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        AddContactCommand request,
        CancellationToken cancellationToken)
    {
        var owner = await ownerRepository.GetOwnerById(new OwnerId(request.OwnerId)) ??
                    throw new OwnerNotFoundException(
                        $"Owner with id(email) {request.OwnerId} not found",
                        new {Id = request.OwnerId});

        var contact = new OwnerContact
        {
            Contact = request.Contact,
            ContactType = request.ContactType
        };

        owner.Contacts.Add(contact);

        await ownerRepository.UpdateOwner(owner.Id, owner);

        return VoidResponseDto.Executed;
    }
}
