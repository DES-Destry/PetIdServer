using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Exceptions;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Exceptions.Owner;

namespace PetIdServer.Application.Requests.Commands.Owner.Registration;

public class RegistrationOwnerCommandHandler(
        IOwnerTokenService ownerTokenService, 
        IPasswordService passwordService,
        IOwnerRepository ownerRepository)
    : IRequestHandler<RegistrationOwnerCommand, TokenPairDto>
{
    public async Task<TokenPairDto> Handle(RegistrationOwnerCommand request, CancellationToken cancellationToken)
    {
        var ownerCandidate = await ownerRepository.GetOwnerByEmail(request.Email);

        if (ownerCandidate is not null)
            throw new OwnerAlreadyRegisteredException($"Owner with email {request.Email} already registered",
                new {request.Email});

        var passwordHash = await passwordService.HashPassword(request.Password);

        var creationAttributes =
            new Core.Entities.Owner.CreationAttributes(request.Email, passwordHash, request.Name);
        var owner = new Core.Entities.Owner(creationAttributes);

        var createdOwner = await ownerRepository.CreateOwner(owner) ?? throw new SomethingWentWrongException(new
            {Reason = "Owner created successfully in core, but not saved in infrastructure", Email = owner.Id});
        return await ownerTokenService.GenerateTokens(createdOwner);
    }
}