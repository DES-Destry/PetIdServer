namespace PetIdServer.Persistence.Entities;

public class TagEntity
{
    public required int Id { get; init; }
    public required string Code { get; init; }
    public required string HashCode { get; init; }
    public required long ControlCode { get; init; }
    public required string Status { get; init; }
    public Guid? PetId { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? PetAddedAt { get; init; }
    public DateTime? LastScannedAt { get; init; }

    public PetEntity? PairedPet { get; set; }
    public ICollection<TagReportEntity> Reports { get; init; } = [];
    public ICollection<TagHistoryEntryEntity> History { get; init; } = [];
    public ICollection<TagFeatureEntity> Features { get; init; } = [];
}
