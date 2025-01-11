using PetIdServer.Core.Common;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.User;

namespace PetIdServer.Core.Domain.TagReport;

public class TagReportEntity : Entity<TagReportId>
{
    public TagReportEntity(CreationAttributes creationAttributes) : base(
        (TagReportId)Guid.NewGuid())
    {
        CorruptedTag = creationAttributes.CorruptedTag;
        Reporter = creationAttributes.Reporter;

        CreatedAt = DateTime.UtcNow;
    }

    public TagReportEntity(TagReportId id) : base(id) { }

    public TagEntity CorruptedTag { get; set; }

    public UserEntity Reporter { get; set; }

    public UserEntity? Resolver { get; set; }

    public bool IsResolved => Resolver is not null;

    public DateTime CreatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public void ResolvedBy(UserEntity admin)
    {
        Resolver = admin;
        ResolvedAt = DateTime.UtcNow;
    }

    public record CreationAttributes(TagEntity CorruptedTag, UserEntity Reporter);
}
