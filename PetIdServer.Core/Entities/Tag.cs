using PetIdServer.Core.Common;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Core.Exceptions.Tag;

namespace PetIdServer.Core.Entities;

public class Tag : Entity<TagId>
{
    public Tag(CreationAttributes creationAttributes) : base(creationAttributes.Id)
    {
        Code = creationAttributes.Code;
        ControlCode = creationAttributes.ControlCode;
    }

    public Tag(TagId id) : base(id)
    {
    }

    /// <summary>
    ///     A private code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    public long ControlCode { get; set; }
    public Pet? Pet { get; private set; }

    public bool IsAlreadyInUse => Pet is not null;

    public DateTime CreatedAt { get; set; }

    public DateTime? PetAddedAt { get; set; }

    public DateTime? LastScannedAt { get; set; }

    public void SetupPet(Pet pet)
    {
        if (IsAlreadyInUse) throw new TagAlreadyInUseException($"Tag {Id} is already in use", new {Id, Pet});

        Pet = pet;
    }

    public record CreationAttributes(TagId Id, string Code, long ControlCode);
}