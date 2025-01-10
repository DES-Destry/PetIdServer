using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Common.Exceptions.Auth;
using PetIdServer.Core.Domain.User;
using PetIdServer.Core.Domain.User.Exceptions;

namespace PetIdServer.Application.Domain.User.Commands.Login;

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
        UserEntity userCandidate =
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

        if (!userCandidate.Role.HasPermissionsOf(request.WithPermissionsOf ?? UserRole.LeastPrivileged))
        {
            throw new UserUnauthorizedException("User requests more permissions than allowed",
                                                new
                                                {
                                                    UserId = userCandidate.Id,
                                                    UserHasRole = userCandidate.Role.Name,
                                                    UserRoleHasLevel = userCandidate.Role.Level,
                                                    RequestedRole = request.WithPermissionsOf?.Name ??
                                                                    UserRole.LeastPrivileged.Name,
                                                    RequestedRoleLevel = request.WithPermissionsOf?.Level ??
                                                                         UserRole.LeastPrivileged.Level
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
