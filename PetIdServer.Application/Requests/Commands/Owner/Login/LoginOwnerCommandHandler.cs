using MediatR;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Common.Exceptions.Auth;

namespace PetIdServer.Application.Requests.Commands.Owner.Login;

public class LoginOwnerCommandHandler(
    IOwnerTokenService ownerTokenService,
    IPasswordService passwordService,
    IOwnerRepository ownerRepository)
    : IRequestHandler<LoginOwnerCommand, LoginOwnerResponseDto>
{
    public async Task<LoginOwnerResponseDto> Handle(
        LoginOwnerCommand request,
        CancellationToken cancellationToken)
    {
        var ownerCandidate =
            await ownerRepository.GetOwnerByEmail(request.Email) ??
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                new {request.Email, userType = nameof(Core.Domain.Owner.Owner)});

        if (!await passwordService.ValidatePassword(request.Password, ownerCandidate.Password))
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                new {request.Email, userType = nameof(Core.Domain.Owner.Owner)});

        var tokenPair = await ownerTokenService.GenerateTokens(ownerCandidate);
        return new LoginOwnerResponseDto
        {
            AccessToken = tokenPair.AccessToken,
            RefreshToken = tokenPair.RefreshToken,
            OwnerId = ownerCandidate.Id.Value
        };
    }
}
