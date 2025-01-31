namespace PetIdServer.Persistence.Entities;

public class TagEntity
{
    public required int Id { get; init; }
    public required string Code { get; init; }
    public required string HashCode { get; init; }
    public required long ControlCode { get; init; }
    public Guid? PetId { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? PetAddedAt { get; init; }
    public DateTime? LastScannedAt { get; init; }

    public PetEntity? PairedPet { get; set; } = null;
    public ICollection<TagReportEntity> Reports { get; } = [];
}
