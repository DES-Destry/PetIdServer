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
    private readonly List<TagHistoryEntry> _history = [];
    private readonly List<TagReport> _reports = [];

    private Tag(int id) : base((TagId)id) { }
    public required string PrivateCode { get; init; }

    public required string HashCode { get; init; }

    public long ControlCode { get; private init; } = Random.Shared.NextInt64();

    public TagStatus Status { get; private set; } = TagStatus.Initial;

    public PetId? PetId { get; private set; }

    public bool IsAlreadyInUse => Status == TagStatus.InUse;

    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;

    public DateTime? LastScannedAt { get; private set; }

    public IReadOnlyList<TagReport> Reports => _reports;
    public IReadOnlyList<TagHistoryEntry> History => _history;
    public IReadOnlyList<TagFeature> Features { get; private init; } = [];

    public static Tag CreateNew(CreationAttributes creationAttributes)
    {
        ArgumentNullException.ThrowIfNull(creationAttributes);
        ArgumentException.ThrowIfNullOrWhiteSpace(creationAttributes.HashCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(creationAttributes.PrivateCode);

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
        DateTime? lastScannedAt)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(privateCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(hashCode);

        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(features);

        if (controlCode == 0)
        {
            throw new ArgumentException("Control code cannot be default value", nameof(controlCode));
        }

        if (createdAt == default)
        {
            throw new ArgumentException("Created at cannot be default value", nameof(createdAt));
        }

        features ??= TagFeature.DefaultSetOfFeatures;

        return new Tag(id)
        {
            PrivateCode = privateCode,
            HashCode = hashCode,
            ControlCode = controlCode,
            PetId = petId,
            Features = ImmutableList.CreateRange(features),
            CreatedAt = createdAt,
            LastScannedAt = lastScannedAt
        };
    }

    public void ProduceBy(UserId initiatorId)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);

        if (Status != TagStatus.Registered)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not ready to produce", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.Registered
            });
        }

        SetStatusByAndSave(initiatorId, TagStatus.Produced);
    }

    public void SendToExternalRetailerBy(UserId initiatorId)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);

        if (Status != TagStatus.Produced)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not even produced", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.Produced
            });
        }

        SetStatusByAndSave(initiatorId, TagStatus.SentToExternalRetailer);
    }

    public void SendToStore(UserId initiatorId)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);

        if (Status != TagStatus.Produced)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not even produced", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.Produced
            });
        }

        SetStatusByAndSave(initiatorId, TagStatus.GoingToStore);
    }

    public void ArriveToStore(UserId initiatorId)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);

        if (Status != TagStatus.GoingToStore)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not going to store", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.GoingToStore
            });
        }

        SetStatusByAndSave(initiatorId, TagStatus.InStore);
    }

    public void Sell(UserId initiatorId)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);

        if (Status != TagStatus.InStore)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not in store", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.InStore
            });
        }

        SetStatusByAndSave(initiatorId, TagStatus.Sold);
    }

    public void RestoreAfterPetRemoval(UserId initiatorId, TagStatus status)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);
        ArgumentNullException.ThrowIfNull(status);

        if (status != TagStatus.ClearedByAdmin)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not cleared by admin", new
            {
                Id, CurrentStatus = status, RequiredStatus = TagStatus.ClearedByAdmin
            });
        }

        SetStatusByAndSave(initiatorId, status);
    }

    public void RestoreAfterSomethingWentWrong(UserId initiatorId, TagStatus status)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);
        ArgumentNullException.ThrowIfNull(status);

        if (status != TagStatus.Unknown || status != TagStatus.Destroyed)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not unknown or destroyed", new
            {
                Id, CurrentStatus = status, RequiredStatus = (ImmutableArray<TagStatus>) [TagStatus.Unknown, TagStatus.Destroyed]
            });
        }

        SetStatusByAndSave(initiatorId, status);
    }

    public void PairWithPet(PetId petId, UserId newOwnerId)
    {
        ArgumentNullException.ThrowIfNull(newOwnerId);
        ArgumentNullException.ThrowIfNull(petId);

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

        PetId = petId;
        SetStatusByAndSave(newOwnerId, TagStatus.InUse);
    }

    public void RemovePet(UserId initiatorId, TagStatus? status = null)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);

        if (Status != TagStatus.InUse)
        {
            throw new TagIsNotInAppropriateStatusException($"Tag {Id} is not in use", new
            {
                Id, CurrentStatus = Status, RequiredStatus = TagStatus.InUse
            });
        }

        PetId = null;
        SetStatusByAndSave(initiatorId, status ?? TagStatus.ClearedByAdmin);
    }

    public void Destroy(UserId initiatorId)
    {
        ArgumentNullException.ThrowIfNull(initiatorId);
        SetStatusByAndSave(initiatorId, TagStatus.Destroyed);
    }

    public void ReportBy(UserId reporterId)
    {
        ArgumentNullException.ThrowIfNull(reporterId);
        _reports.Add(TagReport.CreateNew(new TagReport.CreationAttributes(reporterId)));
    }

    public void ResolveReportBy(TagReportId reportId, UserId resolverId)
    {
        ArgumentNullException.ThrowIfNull(reportId);
        ArgumentNullException.ThrowIfNull(resolverId);

        TagReport report = _reports.FirstOrDefault(report => report.Id == reportId) ??
                           throw new TagReportNotFoundException(new
                           {
                               tagId = Id, reportId
                           });

        report.ResolvedBy(resolverId);
    }

    private void SetStatusByAndSave(UserId initiatorId, TagStatus newStatus)
    {
        TagHistoryEntry.CreationAttributes entryData = new()
        {
            StatusFrom = Status, StatusTo = newStatus, InitiatorId = initiatorId
        };
        TagHistoryEntry entry = TagHistoryEntry.CreateNew(entryData);

        Status = newStatus;

        _history.Add(entry);
    }


    public record CreationAttributes(
        TagId Id,
        string PrivateCode,
        string HashCode,
        IEnumerable<TagFeature>? Features = null);
}
