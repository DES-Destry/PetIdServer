using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Users.Dto.Tokens;
using PetIdServer.Application.Users.Services;
using PetIdServer.Core.Users;
using PetIdServer.Core.Users.Exceptions;
using PetIdServer.Core.Users.Exceptions.Auth;

namespace PetIdServer.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IUserTokenService userTokenService,
    IHashService hashService)
    : IRequestHandler<ChangePasswordCommand, TokenPairDto>
{
    public async Task<TokenPairDto> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        User user = await userRepository.GetUserById((UserId)request.Id) ??
                    throw new UserNotFoundException($"User {request.Id} not found!",
                                                    new
                                                    {
                                                        UserId = request.Id, UseCase = nameof(ChangePasswordCommand)
                                                    });

        if (user.Password != null && !string.IsNullOrWhiteSpace(user.Password))
        {
            if (request.OldPassword is null)
            {
                throw new IncorrectCredentialsException("You must provide an old password!", new
                {
                    UserId = request.Id, UseCase = nameof(ChangePasswordCommand)
                });
            }

            bool passwordIsValid = await hashService.Validate(request.OldPassword, user.Password);

            if (!passwordIsValid)
            {
                throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Id}", new
                {
                    UserId = request.Id, UseCase = nameof(ChangePasswordCommand)
                });
            }
        }


        PasswordHash newHashedPassword = await hashService.Hash(request.NewPassword);
        user.ChangePasswordHash(newHashedPassword);
        await userRepository.UpdateUser(user);

        return await userTokenService.GenerateTokens(user);
    }
}
