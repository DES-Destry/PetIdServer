using PetIdServer.Core.Common;
using PetIdServer.Core.Users;

namespace PetIdServer.Core.TagReports;

public class TagReport : Entity<TagReportId>
{
    private TagReport() : base((TagReportId)Guid.NewGuid()) { }

    public required UserId ReporterId { get; init; }
    public UserId? ResolverId { get; private set; }

    public bool IsResolved => ResolverId is not null;

    public DateTime CreatedAt { get; private init; }

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

    public void ResolvedBy(UserId adminId)
    {
        ResolverId = adminId;
        ResolvedAt = DateTime.UtcNow;
    }

    public record CreationAttributes(UserId ReporterId);
}
