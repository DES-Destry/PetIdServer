using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.User.Dto.Tokens;
using PetIdServer.Application.User.Services;
using PetIdServer.Core.Domain.User;
using PetIdServer.Core.Domain.User.Exceptions;

namespace PetIdServer.Application.User.Commands.Registration;

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
        UserEntity? userCandidate = await userRepository.GetUserByEmail(request.Email);

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
        UserEntity.CreationAttributes creationAttributes = new(request.Email, passwordHash, request.Name);
        UserEntity user = new(creationAttributes);

        await userRepository.CreateUser(user);
        return await userTokenService.GenerateTokens(user);
    }
}
