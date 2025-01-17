using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Users;

public record UserId(Guid Value)
{
    [return: NotNullIfNotNull("adminId")]
    public static implicit operator Guid?(UserId? adminId) => adminId?.Value;

    [return: NotNullIfNotNull("id")]
    public static explicit operator UserId?(Guid? id) => id is null ? null : new UserId(id.Value);
}
