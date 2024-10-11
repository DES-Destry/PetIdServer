using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Common.Exceptions.Auth;
using PetIdServer.Core.Domain.Owner;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;

public class LoginOwnerCommandHandler(
    IOwnerTokenService ownerTokenService,
    IHashService hashService,
    IOwnerRepository ownerRepository)
    : IRequestHandler<LoginOwnerCommand, LoginOwnerResponseDto>
{
    public async Task<LoginOwnerResponseDto> Handle(
        LoginOwnerCommand request,
        CancellationToken cancellationToken)
    {
        OwnerEntity ownerCandidate =
            await ownerRepository.GetOwnerByEmail(request.Email) ??
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                new
                {
                    request.Email, userType = nameof(OwnerEntity)
                });

        if (!await hashService.Validate(request.Password, ownerCandidate.Password))
        {
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                new
                {
                    request.Email, userType = nameof(OwnerEntity)
                });
        }

        TokenPairDto tokenPair = await ownerTokenService.GenerateTokens(ownerCandidate);
        return new LoginOwnerResponseDto
        {
            AccessToken = tokenPair.AccessToken, RefreshToken = tokenPair.RefreshToken, OwnerId = ownerCandidate.Id
        };
    }
}
