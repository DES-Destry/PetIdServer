using PetIdServer.Infrastructure.Database.Domain.Admin;

namespace PetIdServer.Infrastructure.Database.Domain.Tag;

public class TagReportModel
{
    public required Guid Id { get; init; }
    public required int CorruptedTagId { get; init; }
    public required string ReporterId { get; init; }
    public string? ResolverId { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? ResolvedAt { get; init; }

    public TagModel CorruptedTag { get; } = null!;
    public AdminModel Reporter { get; } = null!;
    public AdminModel? Resolver { get; } = null;
}
