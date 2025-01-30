using PetIdServer.Application.Users.Commands.Update;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Users.Mapper;

public static class UserMapper
{
    public static User MapUpdate(this User user, UpdateUserCommand dto)
    {
        user.Update(new User.UpdateAttributes
        {
            Name = dto.Name,
            Address = dto.Address,
            Description = dto.Description
        });

        return user;
    }
}
