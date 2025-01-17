using PetIdServer.Core.Common;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Core.Tags;

public class Tag : Entity<TagId>
{
    private Tag(int id) : base((TagId)id) { }
    public required string PrivateCode { get; init; }

    public required string HashCode { get; init; }

    public long ControlCode { get; init; } = Random.Shared.NextInt64();

    // TODO Delete
    public Pet? Pet { get; private set; }
    public PetId? PetId { get; private set; }

    public bool IsAlreadyInUse => PetId is not null;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? PetAddedAt { get; private set; }

    public DateTime? LastScannedAt { get; private set; }

    public static Tag CreateNew(CreationAttributes creationAttributes)
    {
        return new Tag(creationAttributes.Id)
        {
            PrivateCode = creationAttributes.PrivateCode, HashCode = creationAttributes.HashCode
        };
    }

    public static Tag CreateFromPersistence(TagId id,
        string privateCode,
        string hashCode,
        long controlCode,
        PetId? petId,
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
            CreatedAt = createdAt,
            PetAddedAt = petAddedAt,
            LastScannedAt = lastScannedAt
        };
    }

    public void SetupPet(PetId petId)
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

    public record CreationAttributes(TagId Id, string PrivateCode, string HashCode);
}
