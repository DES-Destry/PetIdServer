using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Owner.Exceptions;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;

public class RegistrationOwnerCommandHandler(
    IOwnerTokenService ownerTokenService,
    IPasswordService passwordService,
    IOwnerRepository ownerRepository)
    : IRequestHandler<RegistrationOwnerCommand, TokenPairDto>
{
    public async Task<TokenPairDto> Handle(
        RegistrationOwnerCommand request,
        CancellationToken cancellationToken)
    {
        var ownerCandidate = await ownerRepository.GetOwnerByEmail(request.Email);

        if (ownerCandidate is not null)
            throw new OwnerAlreadyRegisteredException(
                $"Owner with email {request.Email} already registered",
                new {request.Email});

        var passwordHash = await passwordService.HashPassword(request.Password);

        var creationAttributes =
            new OwnerEntity.CreationAttributes(
                request.Email,
                passwordHash,
                request.Name,
                request.Address,
                request.Description);
        var owner = new OwnerEntity(creationAttributes);

        await ownerRepository.CreateOwner(owner);
        return await ownerTokenService.GenerateTokens(owner);
    }
}
