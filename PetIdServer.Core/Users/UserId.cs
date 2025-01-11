namespace PetIdServer.Core.Users;

public record UserId(Guid Value)
{
    public static implicit operator Guid(UserId adminId) => adminId.Value;
    public static explicit operator UserId(Guid id) => new(id);
}
