using PetIdServer.Core.Common;
using PetIdServer.Core.Users;

namespace PetIdServer.Core.Pets;

public class Pet : Entity<PetId>
{
    private Pet() : base((PetId)Guid.NewGuid()) { }

    // TODO Delete
    public User? User { get; private set; }

    public UserId OwnerId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Type { get; private set; } = null!;
    public bool Sex { get; private set; }
    public bool IsCastrated { get; private set; }
    public Guid? PhotoId { get; private set; }
    public string? Description { get; private set; }

    public static Pet CreateNew(CreationAttributes creationAttributes)
    {
        return new Pet
        {
            OwnerId = creationAttributes.OwnerId,
            Type = creationAttributes.Type,
            Name = creationAttributes.Name,
            Sex = creationAttributes.Sex,
            IsCastrated = creationAttributes.IsCastrated,
            PhotoId = creationAttributes.PhotoId
        };
    }

    public static Pet CreateFromPersistence(PetId id,
        UserId ownerId,
        string name,
        string type,
        bool sex,
        bool isCastrated,
        Guid? photoId,
        string? description)
    {
        return new Pet
        {
            Id = id,
            OwnerId = ownerId,
            Name = name,
            Type = type,
            Sex = sex,
            IsCastrated = isCastrated,
            PhotoId = photoId,
            Description = description
        };
    }

    public void Update(UpdateAttributes updateAttributes)
    {
        Type = updateAttributes.Type ?? Type;
        Name = updateAttributes.Name ?? Name;
        Sex = updateAttributes.Sex ?? Sex;
        IsCastrated = updateAttributes.IsCastrated ?? IsCastrated;
        PhotoId = updateAttributes.PhotoId ?? PhotoId;
        Description = updateAttributes.Description ?? Description;
    }

    public record CreationAttributes(
        UserId OwnerId,
        string Name,
        string Type,
        bool Sex,
        bool IsCastrated,
        Guid? PhotoId = null
    );

    public record UpdateAttributes
    {
        public string? Type { get; init; }
        public string? Name { get; init; }
        public bool? Sex { get; init; }
        public bool? IsCastrated { get; init; }
        public Guid? PhotoId { get; init; }
        public string? Description { get; init; }
    }
}
