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

        IsResolved = false;
        CreatedAt = DateTime.UtcNow;
    }

    public TagReportEntity(TagReportId id) : base(id) { }

    public TagEntity CorruptedTag { get; set; }

    public AdminEntity Reporter { get; set; }

    public bool IsResolved { get; set; }

    public DateTime CreatedAt { get; set; }

    public record CreationAttributes(TagEntity CorruptedTag, AdminEntity Reporter);
}
