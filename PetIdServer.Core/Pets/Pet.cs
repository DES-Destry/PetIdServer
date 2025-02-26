using PetIdServer.Core.Common;
using PetIdServer.Core.Users;

namespace PetIdServer.Core.Pets;

public sealed class Pet : AggregateRoot<PetId>
{
    private Pet() : base((PetId)Guid.CreateVersion7()) { }

    public UserId OwnerId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Type { get; private set; } = null!;
    public bool Sex { get; private set; }
    public bool IsCastrated { get; private set; }
    public bool CanGoOutside { get; private set; }
    public bool IsLost { get; private set; }
    public Guid? PhotoId { get; private set; }
    public string? Description { get; private set; }

    public static Pet CreateNew(CreationAttributes creationAttributes)
    {
        ArgumentNullException.ThrowIfNull(creationAttributes);

        return new Pet
        {
            OwnerId = creationAttributes.OwnerId,
            Type = creationAttributes.Type,
            Name = creationAttributes.Name,
            Sex = creationAttributes.Sex,
            IsCastrated = creationAttributes.IsCastrated,
            CanGoOutside = creationAttributes.CanGoOutside,
            PhotoId = creationAttributes.PhotoId,
            Description = creationAttributes.Description
        };
    }

    public static Pet CreateFromPersistence(
        PetId id,
        UserId ownerId,
        string name,
        string type,
        bool sex,
        bool isCastrated,
        bool canGoOutside,
        bool isLost,
        Guid? photoId = null,
        string? description = null)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(ownerId);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(type);

        return new Pet
        {
            Id = id,
            OwnerId = ownerId,
            Name = name,
            Type = type,
            Sex = sex,
            IsCastrated = isCastrated,
            CanGoOutside = canGoOutside,
            IsLost = isLost,
            PhotoId = photoId,
            Description = description
        };
    }

    public void Update(UpdateAttributes updateAttributes)
    {
        ArgumentNullException.ThrowIfNull(updateAttributes);

        Type = updateAttributes.Type ?? Type;
        Name = updateAttributes.Name ?? Name;
        Sex = updateAttributes.Sex ?? Sex;
        IsCastrated = updateAttributes.IsCastrated ?? IsCastrated;
        CanGoOutside = updateAttributes.CanGoOutside ?? CanGoOutside;
        PhotoId = updateAttributes.PhotoId ?? PhotoId;
        Description = updateAttributes.Description ?? Description;
    }

    public void MarkAsLost() => IsLost = true;
    public void MarkAsFound() => IsLost = false;

    public record CreationAttributes(
        UserId OwnerId,
        string Name,
        string Type,
        bool Sex,
        bool IsCastrated,
        bool CanGoOutside,
        Guid? PhotoId = null,
        string? Description = ""
    );

    public record UpdateAttributes
    {
        public string? Type { get; init; }
        public string? Name { get; init; }
        public bool? Sex { get; init; }
        public bool? IsCastrated { get; init; }
        public bool? CanGoOutside { get; init; }
        public Guid? PhotoId { get; init; }
        public string? Description { get; init; }
    }
}
