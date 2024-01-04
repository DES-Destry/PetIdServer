using PetIdServer.Core.Common;

namespace PetIdServer.Core.Domains.Report;

public class Report : Entity<Guid>
{
    public Report(CreationAttributes creationAttributes) : base(Guid.NewGuid())
    {
        CorruptedTag = creationAttributes.CorruptedTag;
        Reporter = creationAttributes.Reporter;

        IsResolved = false;
        CreatedAt = DateTime.UtcNow;
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Report(Guid id) : base(id) { }

    public Tag.Tag CorruptedTag { get; set; }

    public Admin.Admin Reporter { get; set; }

    public bool IsResolved { get; set; }

    public DateTime CreatedAt { get; set; }

    public record CreationAttributes(Tag.Tag CorruptedTag, Admin.Admin Reporter);
}
