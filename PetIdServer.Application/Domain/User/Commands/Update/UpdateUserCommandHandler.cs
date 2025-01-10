using AutoMapper;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.User;

namespace PetIdServer.Application.Domain.User.Commands.Update;

public class UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        UserEntity? updatedUser = mapper.Map<UpdateUserCommand, UserEntity>(request);
        await userRepository.UpdateUser(updatedUser);

        return VoidResponseDto.Executed;
    }
}
