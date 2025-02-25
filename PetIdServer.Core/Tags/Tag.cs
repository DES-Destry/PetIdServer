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

    public TagStatus Status { get; private set; } = TagStatus.Initial;

    public PetId? PetId { get; private set; }

    public bool IsAlreadyInUse => Status == TagStatus.InUse;

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

    public void Produce()
    {
        if (Status != TagStatus.Registered)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not ready to produce", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.Registered
            });
        }

        Status = TagStatus.Produced;
    }

    public void SendToExternalRetailer()
    {
        if (Status != TagStatus.Produced)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not even produced", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.Produced
            });
        }

        Status = TagStatus.SentToExternalRetailer;
    }

    public void SendToStore()
    {
        if (Status != TagStatus.Produced)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not even produced", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.Produced
            });
        }

        Status = TagStatus.GoingToStore;
    }

    public void ArriveToStore()
    {
        if (Status != TagStatus.GoingToStore)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not going to store", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.GoingToStore
            });
        }

        Status = TagStatus.InStore;
    }

    public void Sell()
    {
        if (Status != TagStatus.InStore)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not in store", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.InStore
            });
        }

        Status = TagStatus.Sold;
    }

    public void RestoreAfterPetRemoval(TagStatus status)
    {
        if (status != TagStatus.ClearedByAdmin)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not cleared by admin", new
            {
                Id, CurrentStatus = status, RequiredStatus = TagStatus.ClearedByAdmin
            });
        }

        Status = status;
    }

    public void RestoreAfterSomethingWentWrong(TagStatus status)
    {
        if (status != TagStatus.Unknown || status != TagStatus.Destroyed)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not unknown or destroyed", new
            {
                Id, CurrentStatus = status, RequiredStatus = (ImmutableArray<TagStatus>) [TagStatus.Unknown, TagStatus.Destroyed]
            });
        }

        Status = status;
    }

    public void PairWithPet(PetId petId)
    {
        if (IsAlreadyInUse)
        {
            throw new TagAlreadyInUseException($"Tag {Id} is already in use with {PetId}", new
            {
                Id, PetId, Status
            });
        }

        if (!TagStatus.ReadyToUse.Contains(Status))
        {
            throw new TagIsNotReadyToPairException($"Tag {Id} is not ready to use", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.ReadyToUse
            });
        }

        Status = TagStatus.InUse;

        PetId = petId;
        PetAddedAt = DateTime.UtcNow;
    }

    public void RemovePet(TagStatus? status = null)
    {
        if (Status != TagStatus.InUse)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not in use", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.InUse
            });
        }

        Status = status ?? TagStatus.ClearedByAdmin;

        PetId = null;
        PetAddedAt = null;
    }

    public void Destroy()
    {
        Status = TagStatus.Destroyed;
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
