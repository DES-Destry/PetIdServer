using PetIdServer.Core.Common;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Users;

namespace PetIdServer.Core.TagReports;

public class TagReport : Entity<TagReportId>
{
    private TagReport() : base((TagReportId)Guid.NewGuid()) { }

    // TODO remove (DDD)
    public Tag CorruptedTag { get; set; } = null!;

    // TODO remove (DDD)
    public User Reporter { get; set; } = null!;

    // TODO remove (DDD)
    public User? Resolver { get; set; }

    public required UserId ReporterId { get; init; }
    public UserId? ResolverId { get; private set; }

    public bool IsResolved => Resolver is not null;

    public DateTime CreatedAt { get; init; }

    public DateTime? ResolvedAt { get; private set; }

    public static TagReport CreateNew(CreationAttributes creationAttributes)
    {
        return new TagReport
        {
            ReporterId = creationAttributes.ReporterId, CreatedAt = DateTime.UtcNow
        };
    }

    public static TagReport CreateFromPersistence(TagReportId reportId,
        UserId reporterId,
        UserId? resolverId,
        DateTime createdAt,
        DateTime? resolvedAt)
    {
        return new TagReport
        {
            Id = reportId,
            ReporterId = reporterId,
            ResolverId = resolverId,
            CreatedAt = createdAt,
            ResolvedAt = resolvedAt
        };
    }

    public void ResolvedBy(User admin)
    {
        Resolver = admin;
        ResolverId = admin.Id;
        ResolvedAt = DateTime.UtcNow;
    }

    public record CreationAttributes(UserId ReporterId);
}
