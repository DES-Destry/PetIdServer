namespace PetIdServer.Core.Domain.Pet;

public record PetId(Guid Value)
{
    public static implicit operator Guid(PetId adminId) => adminId.Value;
    public static explicit operator PetId(Guid id) => new(id);
}
