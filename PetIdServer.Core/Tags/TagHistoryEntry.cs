using PetIdServer.Core.Common;
using PetIdServer.Core.Users;

namespace PetIdServer.Core.Tags;

public class TagHistoryEntry : Entity<TagHistoryEntryId>
{
    private TagHistoryEntry() : base((TagHistoryEntryId)Guid.CreateVersion7()) { }

    public required TagStatus StatusFrom { get; init; }
    public required TagStatus StatusTo { get; init; }

    public required UserId InitiatorId { get; init; }

    public DateTime ChangedAt { get; private init; } = DateTime.UtcNow;

    public static TagHistoryEntry CreateNew(CreationAttributes creationAttributes)
    {
        return new TagHistoryEntry
        {
            StatusFrom = creationAttributes.StatusFrom,
            StatusTo = creationAttributes.StatusTo,
            InitiatorId = creationAttributes.InitiatorId
        };
    }

    public static TagHistoryEntry CreateFromPersistence(TagHistoryEntryId id,
        TagStatus statusFrom,
        TagStatus statusTo,
        UserId initiatorId,
        DateTime changedAt)
    {
        return new TagHistoryEntry
        {
            Id = id,
            StatusFrom = statusFrom,
            StatusTo = statusTo,
            InitiatorId = initiatorId,
            ChangedAt = changedAt
        };
    }

    public record CreationAttributes
    {
        public required TagStatus StatusFrom { get; init; }
        public required TagStatus StatusTo { get; init; }
        public required UserId InitiatorId { get; init; }
    }
}
