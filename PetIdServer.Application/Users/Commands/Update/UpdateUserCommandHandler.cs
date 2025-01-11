using AutoMapper;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Users.Commands.Update;

public class UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        User? updatedUser = mapper.Map<UpdateUserCommand, User>(request);
        await userRepository.UpdateUser(updatedUser);

        return VoidResponseDto.Executed;
    }
}
