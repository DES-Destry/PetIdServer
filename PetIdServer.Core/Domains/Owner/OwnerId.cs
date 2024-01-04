namespace PetIdServer.Core.Domains.Owner;

/// <summary>
///     Owner Id
/// </summary>
/// <param name="Value">Also used as email of owner</param>
public record OwnerId(string Value)
{
    public static implicit operator string(OwnerId adminId)
    {
        return adminId.Value;
    }

    public static explicit operator OwnerId(string id)
    {
        return new OwnerId(id);
    }
}