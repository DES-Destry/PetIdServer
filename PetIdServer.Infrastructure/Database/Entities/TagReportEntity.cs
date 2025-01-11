namespace PetIdServer.Infrastructure.Database.Entities;

public class TagReportEntity
{
    public required Guid Id { get; init; }
    public required int CorruptedTagId { get; init; }
    public required Guid ReporterId { get; init; }
    public Guid? ResolverId { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? ResolvedAt { get; init; }

    public TagEntity CorruptedTag { get; } = null!;
    public UserEntity Reporter { get; } = null!;
    public UserEntity? Resolver { get; } = null;
}
