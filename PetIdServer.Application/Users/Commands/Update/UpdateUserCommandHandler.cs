using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Users.Mapper;
using PetIdServer.Core.Users;
using PetIdServer.Core.Users.Exceptions;

namespace PetIdServer.Application.Users.Commands.Update;

public class UpdateUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        User user = await userRepository.GetUserById((UserId)request.Id) ??
                    throw new UserNotFoundException("User not found",
                                                    new
                                                    {
                                                        UseCase = nameof(UpdateUserCommand),
                                                        UserId = request.Id
                                                    });
        user = user.MapUpdate(request);
        await userRepository.UpdateUser(user);

        return VoidResponseDto.Executed;
    }
}
