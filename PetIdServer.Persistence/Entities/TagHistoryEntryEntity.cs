namespace PetIdServer.Persistence.Entities;

public class TagHistoryEntryEntity
{
    public required Guid Id { get; init; }
    public required int TagId { get; init; }
    public required Guid? InitiatorId { get; init; }

    public required string StatusFrom { get; init; }
    public required string StatusTo { get; init; }
    public required DateTime ChangedAt { get; init; }

    public TagEntity? RelatedTag { get; init; }
    public UserEntity? Initiator { get; init; }
}
