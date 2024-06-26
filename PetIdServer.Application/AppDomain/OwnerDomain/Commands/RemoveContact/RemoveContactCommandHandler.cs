using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Owner.Exceptions;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.RemoveContact;

public class RemoveContactCommandHandler(IOwnerRepository ownerRepository)
    : IRequestHandler<RemoveContactCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        RemoveContactCommand request,
        CancellationToken cancellationToken)
    {
        var owner = await ownerRepository.GetOwnerByEmail(request.OwnerEmail) ??
                    throw new OwnerNotFoundException(
                        $"Owner with email {request.OwnerEmail} not found",
                        new {Email = request.OwnerEmail});

        owner.Contacts = owner.Contacts
            .Where(contact => contact.ContactType != request.ContactType)
            .ToList();

        return VoidResponseDto.Executed;
    }
}
