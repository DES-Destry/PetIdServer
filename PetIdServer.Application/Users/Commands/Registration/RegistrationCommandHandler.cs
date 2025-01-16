using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Users.Dto.Tokens;
using PetIdServer.Application.Users.Services;
using PetIdServer.Core.Users;
using PetIdServer.Core.Users.Exceptions;

namespace PetIdServer.Application.Users.Commands.Registration;

public class RegistrationCommandHandler(
    IUserTokenService userTokenService,
    IHashService hashService,
    IUserRepository userRepository)
    : IRequestHandler<RegistrationCommand, TokenPairDto>
{
    public async Task<TokenPairDto> Handle(
        RegistrationCommand request,
        CancellationToken cancellationToken)
    {
        User? userCandidate = await userRepository.GetUserByEmail(request.Email);

        if (userCandidate is not null)
        {
            throw new UserAlreadyRegisteredException(
                $"User with email {request.Email} already registered",
                new
                {
                    UserEmail = request.Email, UseCase = nameof(RegistrationCommand)
                });
        }

        PasswordHash passwordHash = await hashService.Hash(request.Password);

        // Registration creates user with the least privileged role
        User.CreationAttributes creationAttributes = new(request.Email, passwordHash, request.Name);
        User user = User.CreateNew(creationAttributes);

        await userRepository.CreateUser(user);
        return await userTokenService.GenerateTokens(user);
    }
}
