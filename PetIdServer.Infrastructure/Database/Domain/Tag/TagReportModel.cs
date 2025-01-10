using PetIdServer.Infrastructure.Database.Domain.User;

namespace PetIdServer.Infrastructure.Database.Domain.Tag;

public class TagReportModel
{
    public required Guid Id { get; init; }
    public required int CorruptedTagId { get; init; }
    public required Guid ReporterId { get; init; }
    public Guid? ResolverId { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? ResolvedAt { get; init; }

    public TagModel CorruptedTag { get; } = null!;
    public UserModel Reporter { get; } = null!;
    public UserModel? Resolver { get; } = null;
}
