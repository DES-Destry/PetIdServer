using PetIdServer.Core.Common;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Core.Domain.Tag;

public class TagEntity : Entity<TagId>
{
    public TagEntity(CreationAttributes creationAttributes) : base(creationAttributes.Id)
    {
        PrivateCode = creationAttributes.PrivateCode;
        HashCode = creationAttributes.HashCode;

        ControlCode = Random.Shared.NextInt64();
        CreatedAt = DateTime.UtcNow;
    }

    public TagEntity(TagId id) : base(id) { }

    public string PrivateCode { get; init; } = string.Empty;

    public string HashCode { get; init; } = string.Empty;

    public long ControlCode { get; init; }
    public PetEntity? Pet { get; private set; }

    public bool IsAlreadyInUse => Pet is not null;

    public DateTime CreatedAt { get; init; }

    public DateTime? PetAddedAt { get; set; }

    public DateTime? LastScannedAt { get; set; }

    public void SetupPet(PetEntity pet)
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
