using PetIdServer.Core.Common;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Users;

namespace PetIdServer.Core.TagReports;

public class TagReport : Entity<TagReportId>
{
    public TagReport(CreationAttributes creationAttributes) : base(
        (TagReportId)Guid.NewGuid())
    {
        CorruptedTag = creationAttributes.CorruptedTag;
        Reporter = creationAttributes.Reporter;

        CreatedAt = DateTime.UtcNow;
    }

    public TagReport(TagReportId id) : base(id) { }

    public Tag CorruptedTag { get; set; }

    public User Reporter { get; set; }

    public User? Resolver { get; set; }

    public bool IsResolved => Resolver is not null;

    public DateTime CreatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public void ResolvedBy(User admin)
    {
        Resolver = admin;
        ResolvedAt = DateTime.UtcNow;
    }

    public record CreationAttributes(Tag CorruptedTag, User Reporter);
}
