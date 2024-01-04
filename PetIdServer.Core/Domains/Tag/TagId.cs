namespace PetIdServer.Core.Domains.Tag;

public record TagId(int Value)
{
    public static implicit operator int(TagId tagId)
    {
        return tagId.Value;
    }

    public static explicit operator TagId(int id)
    {
        return new TagId(id);
    }
}