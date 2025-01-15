using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class UserMapper
{
    public static User ToCore(this UserEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        // Involve how to create a User from a UserEntity (user creation as rule is a limited for just creating a new user)
        return null;
        // return new User; // ???
    }

    public static UserEntity ToEntity(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserEntity
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString(),
            Password = user.Password,
            Address = user.Address,
            Description = user.Description,
            Contacts = user.Contacts.Select(contact => contact.ToEntity(user.Id)).ToList()
        };
    }
}
