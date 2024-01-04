namespace PetIdServer.Core.Domain.Admin;

/// <summary>
///     Admin Id
/// </summary>
/// <param name="Value">Also used as username of admin</param>
public record AdminId(string Value)
{
    public static implicit operator string(AdminId adminId) => adminId.Value;
    public static explicit operator AdminId(string id) => new(id);
}
