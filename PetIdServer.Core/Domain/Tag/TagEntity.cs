using PetIdServer.Core.Common;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Core.Domain.Tag;

public class TagEntity : Entity<TagId>
{
    public TagEntity(CreationAttributes creationAttributes) : base(creationAttributes.Id)
    {
        Code = creationAttributes.Code;

        ControlCode = Random.Shared.NextInt64();
        CreatedAt = DateTime.UtcNow;
    }

    public TagEntity(TagId id) : base(id) { }

    /// <summary>
    ///     A private code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    public long ControlCode { get; set; }
    public PetEntity? Pet { get; private set; }

    public bool IsAlreadyInUse => Pet is not null;

    public DateTime CreatedAt { get; set; }

    public DateTime? PetAddedAt { get; set; }

    public DateTime? LastScannedAt { get; set; }

    public void SetupPet(PetEntity pet)
    {
        if (IsAlreadyInUse)
            throw new TagAlreadyInUseException($"Tag {Id} is already in use", new {Id, Pet});

        Pet = pet;
    }

    public record CreationAttributes(TagId Id, string Code);
}
