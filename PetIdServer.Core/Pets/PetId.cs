using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Pets;

public record PetId(Guid Value)
{
    [return: NotNullIfNotNull("adminId")]
    public static implicit operator Guid?(PetId? adminId) => adminId?.Value;

    public static implicit operator Guid(PetId adminId) => adminId.Value;


    [return: NotNullIfNotNull("id")]
    public static explicit operator PetId?(Guid? id) => id is null ? null : new PetId(id.Value);

    public static explicit operator PetId(Guid id) => new(id);
}
