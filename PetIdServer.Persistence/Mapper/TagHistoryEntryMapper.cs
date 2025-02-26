using PetIdServer.Core.Tags;
using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class TagHistoryEntryMapper
{
    public static TagHistoryEntry ToCore(this TagHistoryEntryEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return TagHistoryEntry.CreateFromPersistence(
            (TagHistoryEntryId)entity.Id,
            TagStatus.Parse(entity.StatusFrom),
            TagStatus.Parse(entity.StatusTo),
            (UserId?)entity.InitiatorId,
            entity.ChangedAt
        );
    }

    public static TagHistoryEntryEntity ToEntity(this TagHistoryEntry entry, Tag tag)
    {
        ArgumentNullException.ThrowIfNull(entry);

        return new TagHistoryEntryEntity
        {
            Id = entry.Id,
            TagId = tag.Id,
            InitiatorId = entry.InitiatorId,
            StatusFrom = entry.StatusFrom.ToString(),
            StatusTo = entry.StatusTo.ToString(),
            ChangedAt = entry.ChangedAt
        };
    }
}
