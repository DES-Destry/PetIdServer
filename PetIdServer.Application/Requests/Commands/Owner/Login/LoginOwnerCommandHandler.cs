using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Exceptions.Auth;

namespace PetIdServer.Application.Requests.Commands.Owner.Login;

public class LoginOwnerCommandHandler : IRequestHandler<LoginOwnerCommand, LoginOwnerResponseDto>
{
    private readonly IOwnerTokenService _ownerTokenService;
    private readonly IPasswordService _passwordService;
    private readonly IOwnerRepository _ownerRepository;

    public LoginOwnerCommandHandler(IOwnerTokenService ownerTokenService,
        IPasswordService passwordService,
        IOwnerRepository ownerRepository)
    {
        _ownerTokenService = ownerTokenService;
        _passwordService = passwordService;
        _ownerRepository = ownerRepository;
    }

    public async Task<LoginOwnerResponseDto> Handle(LoginOwnerCommand request, CancellationToken cancellationToken)
    {
        var ownerCandidate =
            await _ownerRepository.GetOwnerByEmail(request.Email) ??
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                new {request.Email, userType = nameof(Core.Entities.Owner)});

        if (!await _passwordService.ValidatePassword(request.Password, ownerCandidate.Password))
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                new {request.Email, userType = nameof(Core.Entities.Owner)});

        var tokenPair = await _ownerTokenService.GenerateTokens(ownerCandidate);
        return new LoginOwnerResponseDto
        {
            AccessToken = tokenPair.AccessToken,
            RefreshToken = tokenPair.RefreshToken,
            OwnerId = ownerCandidate.Id.Value
        };
    }
}