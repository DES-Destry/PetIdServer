namespace PetIdServer.Core.Domain.Owner;

/// <summary>
///     Owner Id
/// </summary>
/// <param name="Value">Also used as email of owner</param>
public record OwnerId(Guid Value)
{
    public static implicit operator Guid(OwnerId adminId) => adminId.Value;
    public static explicit operator OwnerId(string id) => new(Guid.Parse(id));
}
