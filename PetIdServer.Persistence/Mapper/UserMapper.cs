using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class UserMapper
{
    public static User ToCore(this UserEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        IList<UserContact> contacts = entity.Contacts.Select(contact => contact.ToCore()).ToList();

        return User.CreateFromPersistence((UserId)entity.Id,
                                          entity.Email,
                                          PasswordHash.FromHash(entity.Password),
                                          entity.Name,
                                          entity.Address,
                                          entity.Description,
                                          UserRole.Parse(entity.Role),
                                          contacts);
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
