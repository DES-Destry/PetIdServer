namespace PetIdServer.Persistence.Entities;

public class TagFeatureEntity
{
    public required int TagId { get; init; }
    public required string Feature { get; init; }

    public TagEntity? Tag { get; init; }
}
