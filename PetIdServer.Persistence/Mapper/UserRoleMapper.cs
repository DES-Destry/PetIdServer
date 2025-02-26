using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class UserRoleMapper
{
    public static UserRole ToCore(this UserRoleEntity role)
    {
        ArgumentNullException.ThrowIfNull(role);
        return UserRole.Parse(role.Role);
    }

    public static UserRoleEntity ToEntity(this UserRole role, User user)
    {
        ArgumentNullException.ThrowIfNull(role);
        ArgumentNullException.ThrowIfNull(user);

        return new UserRoleEntity
        {
            UserId = user.Id, Role = role.ToString()
        };
    }
}
