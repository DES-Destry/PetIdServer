using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class UserMapper
{
    public static User ToCore(this UserEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        IEnumerable<UserContact> contacts = entity.Contacts.Select(UserContactMapper.ToCore);
        IEnumerable<UserRole> roles = entity.Roles.Select(UserRoleMapper.ToCore);

        return User.CreateFromPersistence(
            (UserId)entity.Id,
            entity.Email,
            PasswordHash.FromHash(entity.Password),
            entity.Name,
            entity.Address,
            entity.Description,
            roles,
            contacts);
    }

    public static UserEntity ToEntity(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        ICollection<UserContactEntity> contacts = user.Contacts.Select(contact => contact.ToEntity(user)).ToList();
        ICollection<UserRoleEntity> roles = user.Roles.Select(role => role.ToEntity(user)).ToList();

        return new UserEntity
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Address = user.Address,
            Description = user.Description,
            Contacts = contacts,
            Roles = roles
        };
    }
}
