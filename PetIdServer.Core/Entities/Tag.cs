using PetIdServer.Core.Common;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Core.Exceptions.Tag;

namespace PetIdServer.Core.Entities;

public class Tag : Entity<TagId>
{
    public Pet? Pet { get; private set; }

    public bool IsAlreadyInUse => Pet is not null;

    public Tag(TagId id) : base(id)
    {
    }

    public void SetupPet(Pet pet)
    {
        if (IsAlreadyInUse) throw new TagAlreadyInUseException($"Tag {Id} is already in use", new {Id, Pet});

        Pet = pet;
    }
}