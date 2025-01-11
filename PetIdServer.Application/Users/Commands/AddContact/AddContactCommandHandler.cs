using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Users;
using PetIdServer.Core.Users.Exceptions;

namespace PetIdServer.Application.Users.Commands.AddContact;

public class AddContactCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddContactCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        AddContactCommand request,
        CancellationToken cancellationToken)
    {
        User user = await userRepository.GetUserById((UserId)request.UserId) ??
                    throw new UserNotFoundException(
                        $"User with id {request.UserId} not found",
                        new
                        {
                            Id = request.UserId, UseCase = nameof(AddContactCommand)
                        });

        UserContact contact = new()
        {
            Contact = request.Contact, ContactType = request.ContactType
        };

        user.Contacts.Add(contact);

        await userRepository.UpdateUser(user);

        return VoidResponseDto.Executed;
    }
}
