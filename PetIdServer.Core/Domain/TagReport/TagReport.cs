using PetIdServer.Core.Common;

namespace PetIdServer.Core.Domain.TagReport;

public class TagReport : Entity<TagReportId>
{
    public TagReport(CreationAttributes creationAttributes) : base(new TagReportId(Guid.NewGuid()))
    {
        CorruptedTag = creationAttributes.CorruptedTag;
        Reporter = creationAttributes.Reporter;

        IsResolved = false;
        CreatedAt = DateTime.UtcNow;
    }

    public TagReport(TagReportId id) : base(id) { }

    public Tag.Tag CorruptedTag { get; set; }

    public Admin.Admin Reporter { get; set; }

    public bool IsResolved { get; set; }

    public DateTime CreatedAt { get; set; }

    public record CreationAttributes(Tag.Tag CorruptedTag, Admin.Admin Reporter);
}
