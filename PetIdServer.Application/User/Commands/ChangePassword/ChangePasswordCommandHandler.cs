using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.User.Dto.Tokens;
using PetIdServer.Application.User.Services;
using PetIdServer.Core.Domain.User;
using PetIdServer.Core.Domain.User.Exceptions;
using PetIdServer.Core.User.Exceptions.Auth;

namespace PetIdServer.Application.User.Commands.ChangePassword;

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
        UserEntity user = await userRepository.GetUserById((UserId)request.Id) ??
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
