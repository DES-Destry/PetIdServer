using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Exceptions.Auth;

namespace PetIdServer.Application.Requests.Commands.Owner.Login;

public class LoginOwnerCommandHandler : IRequestHandler<LoginOwnerCommand, TokenPairDto>
{
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly IOwnerRepository _ownerRepository;

    public LoginOwnerCommandHandler(ITokenService tokenService,
        IPasswordService passwordService,
        IOwnerRepository ownerRepository)
    {
        _tokenService = tokenService;
        _passwordService = passwordService;
        _ownerRepository = ownerRepository;
    }

    public async Task<TokenPairDto> Handle(LoginOwnerCommand request, CancellationToken cancellationToken)
    {
        var ownerCandidate =
            await _ownerRepository.GetOwnerByEmail(request.Email) ??
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                new {request.Email});

        if (!await _passwordService.ValidatePassword(request.Password, ownerCandidate.Password))
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                new {request.Email});

        return await _tokenService.GenerateTokens(ownerCandidate);
    }
}