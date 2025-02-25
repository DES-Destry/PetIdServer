using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Tags;

public record TagHistoryEntryId(Guid Value)
{
    [return: NotNullIfNotNull("entityId")]
    public static implicit operator Guid?(TagHistoryEntryId? entityId) => entityId?.Value;

    [return: NotNullIfNotNull("guid")]
    public static explicit operator TagHistoryEntryId?(Guid? guid) => guid is null ? null : new TagHistoryEntryId(guid.Value);
}
