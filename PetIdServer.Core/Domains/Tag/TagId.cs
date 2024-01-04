namespace PetIdServer.Core.Domains.Tag;

public record TagId(int Value)
{
    public static implicit operator int(TagId tagId) => tagId.Value;
    public static explicit operator TagId(int id) => new(id);
}
