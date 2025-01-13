using PetIdServer.Core.Common;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Core.Tags;

public class Tag(Tag.CreationAttributes creationAttributes) : Entity<TagId>(creationAttributes.Id)
{
    public string PrivateCode { get; init; } = creationAttributes.PrivateCode;

    public string HashCode { get; init; } = creationAttributes.HashCode;

    public long ControlCode { get; init; } = Random.Shared.NextInt64();
    public Pet? Pet { get; private set; }

    public bool IsAlreadyInUse => Pet is not null;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? PetAddedAt { get; private set; }

    public DateTime? LastScannedAt { get; private set; }

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
