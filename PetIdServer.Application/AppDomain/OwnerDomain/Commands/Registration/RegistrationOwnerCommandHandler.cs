using MediatR;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Owner.Exceptions;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;

public class RegistrationOwnerCommandHandler(
    IOwnerTokenService ownerTokenService,
    IPasswordService passwordService,
    IOwnerRepository ownerRepository)
    : IRequestHandler<RegistrationOwnerCommand, LoginOwnerResponseDto>
{
    public async Task<LoginOwnerResponseDto> Handle(
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
        var tokenPair = await ownerTokenService.GenerateTokens(owner);

        return new LoginOwnerResponseDto
        {
            AccessToken = tokenPair.AccessToken,
            RefreshToken = tokenPair.RefreshToken,
            OwnerId = owner.Id
        };
    }
}
