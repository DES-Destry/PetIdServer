namespace PetIdServer.Persistence.Entities;

public class UserRoleEntity
{
    public required Guid UserId { get; init; }
    public required string Role { get; init; }

    public UserEntity? User { get; init; }
}
