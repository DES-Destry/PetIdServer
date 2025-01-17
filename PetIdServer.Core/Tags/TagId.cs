using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Tags;

public record TagId(int Value)
{
    [return: NotNullIfNotNull("tagId")]
    public static implicit operator int?(TagId? tagId) => tagId?.Value;

    public static implicit operator int(TagId tagId) => tagId.Value;


    [return: NotNullIfNotNull("id")]
    public static explicit operator TagId?(int? id) => id is null ? null : new TagId(id.Value);

    public static explicit operator TagId(int id) => new(id);
}
