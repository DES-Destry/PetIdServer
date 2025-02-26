using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Users.Dto.Tokens;
using PetIdServer.Application.Users.Services;
using PetIdServer.Core.Users;
using PetIdServer.Core.Users.Exceptions;
using PetIdServer.Core.Users.Exceptions.Auth;

namespace PetIdServer.Application.Users.Commands.Login;

public class LoginCommandHandler(
    IUserTokenService userTokenService,
    IHashService hashService,
    IUserRepository userRepository)
    : IRequestHandler<LoginCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        UserRole requestedRole = request.WithPermissionsOf is null
            ? UserRole.LeastPrivileged
            : UserRole.Parse(request.WithPermissionsOf);

        User userCandidate =
            await userRepository.GetUserByEmail(request.Email) ??
            throw new UserNotFoundException($"User with email {request.Email} not found",
                                            new
                                            {
                                                UserEmail = request.Email, useCase = nameof(LoginCommand)
                                            });

        if (userCandidate.Password is not null && !await hashService.Validate(request.Password, userCandidate.Password))
        {
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Email}",
                                                    new
                                                    {
                                                        UserEmail = request.Email, useCase = nameof(LoginCommand)
                                                    });
        }

        if (!userCandidate.HasPermissionsOf(requestedRole))
        {
            throw new UserUnauthorizedException("User requests more permissions than allowed",
                                                new
                                                {
                                                    UserId = userCandidate.Id,
                                                    UserHasRole = userCandidate.Roles.Select(r => r.Name),
                                                    RequestedRole = requestedRole.Name
                                                });
        }

        // TODO: generate token with permissions of request.WithPermissionsOf
        TokenPairDto tokenPair = await userTokenService.GenerateTokens(userCandidate);
        return new LoginResponseDto
        {
            AccessToken = tokenPair.AccessToken, RefreshToken = tokenPair.RefreshToken, UserId = userCandidate.Id
        };
    }
}
