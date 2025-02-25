using System.Collections.Immutable;
using PetIdServer.Core.Common;
using PetIdServer.Core.Pets;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.TagReports.Exceptions;
using PetIdServer.Core.Tags.Exceptions;
using PetIdServer.Core.Users;

namespace PetIdServer.Core.Tags;

public sealed class Tag : AggregateRoot<TagId>
{
    private readonly List<TagReport> _reports = [];

    private Tag(int id) : base((TagId)id) { }
    public required string PrivateCode { get; init; }

    public required string HashCode { get; init; }

    public long ControlCode { get; private init; } = Random.Shared.NextInt64();

    public PetId? PetId { get; private set; }

    public bool IsAlreadyInUse => PetId is not null;

    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;

    public DateTime? PetAddedAt { get; private set; }

    public DateTime? LastScannedAt { get; private set; }

    public IReadOnlyList<TagReport> Reports => _reports;
    public IReadOnlyList<TagFeature> Features { get; private init; } = [];

    public static Tag CreateNew(CreationAttributes creationAttributes)
    {
        IEnumerable<TagFeature> features = creationAttributes.Features ?? TagFeature.DefaultSetOfFeatures;

        return new Tag(creationAttributes.Id)
        {
            PrivateCode = creationAttributes.PrivateCode,
            HashCode = creationAttributes.HashCode,
            Features = ImmutableList.CreateRange(features)
        };
    }

    public static Tag CreateFromPersistence(TagId id,
        string privateCode,
        string hashCode,
        long controlCode,
        PetId? petId,
        IEnumerable<TagFeature> features,
        DateTime createdAt,
        DateTime? petAddedAt,
        DateTime? lastScannedAt)
    {
        return new Tag(id)
        {
            PrivateCode = privateCode,
            HashCode = hashCode,
            ControlCode = controlCode,
            PetId = petId,
            Features = ImmutableList.CreateRange(features),
            CreatedAt = createdAt,
            PetAddedAt = petAddedAt,
            LastScannedAt = lastScannedAt
        };
    }

    public void PairWithPet(PetId petId)
    {
        if (IsAlreadyInUse)
        {
            throw new TagAlreadyInUseException($"Tag {Id} is already in use with {PetId}", new
            {
                Id, PetId
            });
        }

        PetId = petId;
    }

    public void RemovePet()
    {
        PetId = null;
        PetAddedAt = null;
    }

    public void ReportBy(UserId reporterId)
    {
        _reports.Add(TagReport.CreateNew(new TagReport.CreationAttributes(reporterId)));
    }

    public void ResolveReportBy(TagReportId reportId, UserId resolverId)
    {
        TagReport report = _reports.FirstOrDefault(report => report.Id == reportId) ??
                           throw new TagReportNotFoundException(new
                           {
                               tagId = Id, reportId
                           });

        report.ResolvedBy(resolverId);
    }

    public record CreationAttributes(
        TagId Id,
        string PrivateCode,
        string HashCode,
        IEnumerable<TagFeature>? Features = null);
}
