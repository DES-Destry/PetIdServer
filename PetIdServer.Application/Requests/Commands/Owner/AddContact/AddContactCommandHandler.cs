using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Core.Exceptions.Owner;
using PetIdServer.Core.ValueObjects;

namespace PetIdServer.Application.Requests.Commands.Owner.AddContact;

public class AddContactCommandHandler : IRequestHandler<AddContactCommand, VoidResponseDto>
{
    private readonly IOwnerRepository _ownerRepository;

    public AddContactCommandHandler(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<VoidResponseDto> Handle(AddContactCommand request, CancellationToken cancellationToken)
    {
        var owner = await _ownerRepository.GetOwnerById(new OwnerId(request.OwnerId)) ??
                    throw new OwnerNotFoundException($"Owner with id(email) {request.OwnerId} not found",
                        new {Id = request.OwnerId});

        var contact = new OwnerContact
        {
            Contact = request.Contact,
            ContactType = request.ContactType,
        };

        owner.Contacts.Add(contact);

        await _ownerRepository.UpdateOwner(owner.Id, owner);

        return VoidResponseDto.Executed;
    }
}