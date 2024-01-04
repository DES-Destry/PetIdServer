namespace PetIdServer.Core.Domains.Pet;

public record PetId(Guid Value)
{
    public static implicit operator Guid(PetId adminId)
    {
        return adminId.Value;
    }

    public static explicit operator PetId(Guid id)
    {
        return new PetId(id);
    }
}