using PetIdServer.Core.Common;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Core.Tags;

public class Tag : Entity<TagId>
{
    public Tag(CreationAttributes creationAttributes) : base(creationAttributes.Id)
    {
        PrivateCode = creationAttributes.PrivateCode;
        HashCode = creationAttributes.HashCode;

        ControlCode = Random.Shared.NextInt64();
        CreatedAt = DateTime.UtcNow;
    }

    public Tag(TagId id) : base(id) { }

    public string PrivateCode { get; init; } = string.Empty;

    public string HashCode { get; init; } = string.Empty;

    public long ControlCode { get; init; }
    public Pet? Pet { get; private set; }

    public bool IsAlreadyInUse => Pet is not null;

    public DateTime CreatedAt { get; init; }

    public DateTime? PetAddedAt { get; set; }

    public DateTime? LastScannedAt { get; set; }

    public void SetupPet(Pet pet)
    {
        if (IsAlreadyInUse)
        {
            throw new TagAlreadyInUseException($"Tag {Id} is already in use", new
            {
                Id, Pet
            });
        }

        Pet = pet;
    }

    public void RemovePet()
    {
        Pet = null;
        PetAddedAt = null;
    }

    public record CreationAttributes(TagId Id, string PrivateCode, string HashCode);
}
