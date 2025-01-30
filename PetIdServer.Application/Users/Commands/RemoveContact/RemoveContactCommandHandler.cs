using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Users;
using PetIdServer.Core.Users.Exceptions;

namespace PetIdServer.Application.Users.Commands.RemoveContact;

public class RemoveContactCommandHandler(IUserRepository userRepository)
    : IRequestHandler<RemoveContactCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        RemoveContactCommand request,
        CancellationToken cancellationToken)
    {
        User user = await userRepository.GetUserById((UserId)request.UserId) ??
                    throw new UserNotFoundException(
                        $"User with id {request.UserId} not found",
                        new
                        {
                            request.UserId,
                            UseCase = nameof(RemoveContactCommand)
                        });

        user.RemoveContactWithType(request.ContactType);
        await userRepository.UpdateUser(user);

        return VoidResponseDto.Executed;
    }
}
