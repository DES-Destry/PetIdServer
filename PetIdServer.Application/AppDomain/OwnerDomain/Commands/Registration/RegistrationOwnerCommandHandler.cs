using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Owner.Exceptions;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;

public class RegistrationOwnerCommandHandler(
    IOwnerTokenService ownerTokenService,
    IHashService hashService,
    IOwnerRepository ownerRepository)
    : IRequestHandler<RegistrationOwnerCommand, TokenPairDto>
{
    public async Task<TokenPairDto> Handle(
        RegistrationOwnerCommand request,
        CancellationToken cancellationToken)
    {
        OwnerEntity? ownerCandidate = await ownerRepository.GetOwnerByEmail(request.Email);

        if (ownerCandidate is not null)
        {
            throw new OwnerAlreadyRegisteredException(
                $"Owner with email {request.Email} already registered",
                new
                {
                    request.Email
                });
        }

        string passwordHash = await hashService.Hash(request.Password);

        OwnerEntity.CreationAttributes creationAttributes =
            new OwnerEntity.CreationAttributes(request.Email, passwordHash, request.Name);
        OwnerEntity owner = new OwnerEntity(creationAttributes);

        await ownerRepository.CreateOwner(owner);
        return await ownerTokenService.GenerateTokens(owner);
    }
}
