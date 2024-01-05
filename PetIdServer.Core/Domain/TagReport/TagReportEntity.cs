using PetIdServer.Core.Common;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Core.Domain.TagReport;

public class TagReportEntity : Entity<TagReportId>
{
    public TagReportEntity(CreationAttributes creationAttributes) : base(
        (TagReportId) Guid.NewGuid())
    {
        CorruptedTag = creationAttributes.CorruptedTag;
        Reporter = creationAttributes.Reporter;

        CreatedAt = DateTime.UtcNow;
    }

    public TagReportEntity(TagReportId id) : base(id) { }

    public TagEntity CorruptedTag { get; set; }

    public AdminEntity Reporter { get; set; }

    public AdminEntity? Resolver { get; set; }

    public bool IsResolved => Resolver is not null;

    public DateTime CreatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public void ResolvedBy(AdminEntity admin)
    {
        Resolver = admin;
        ResolvedAt = DateTime.UtcNow;
    }

    public record CreationAttributes(TagEntity CorruptedTag, AdminEntity Reporter);
}
