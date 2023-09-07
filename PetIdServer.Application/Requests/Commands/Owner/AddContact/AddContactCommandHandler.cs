using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
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
        var owner = await _ownerRepository.GetOwnerByEmail(request.OwnerEmail) ?? throw new Exception(); // Owner not found
        var contact = new OwnerContact
        {
            Contact = request.Contact,
            ContactType = request.ContactType,
        };

        owner.Contacts.Add(contact);

        await _ownerRepository.UpdateOwner(owner.Email, owner);
        
        return VoidResponseDto.Executed;
    }
}