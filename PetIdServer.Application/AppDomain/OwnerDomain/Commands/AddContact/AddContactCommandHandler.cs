using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Owner.Exceptions;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.AddContact;

public class AddContactCommandHandler(IOwnerRepository ownerRepository)
    : IRequestHandler<AddContactCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        AddContactCommand request,
        CancellationToken cancellationToken)
    {
        var owner = await ownerRepository.GetOwnerById((OwnerId) request.OwnerId) ??
                    throw new OwnerNotFoundException(
                        $"Owner with id(email) {request.OwnerId} not found",
                        new {Id = request.OwnerId});

        var contact = new OwnerContactVo
        {
            Contact = request.Contact,
            ContactType = request.ContactType
        };

        owner.Contacts.Add(contact);

        await ownerRepository.UpdateOwner(owner.Id, owner);

        return VoidResponseDto.Executed;
    }
}
