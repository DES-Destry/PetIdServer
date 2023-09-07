using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;

namespace PetIdServer.Application.Requests.Commands.Owner.RemoveContact;

public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand, VoidResponseDto>
{
    private readonly IOwnerRepository _ownerRepository;

    public RemoveContactCommandHandler(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<VoidResponseDto> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
    {
        var owner = await _ownerRepository.GetOwnerByEmail(request.OwnerEmail) ?? throw new Exception(); // Owner not found
        owner.Contacts = owner.Contacts.Where(contact => contact.ContactType != request.ContactType).ToList();

        return VoidResponseDto.Executed;
    }
}