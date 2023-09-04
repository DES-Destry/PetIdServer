using PetIdServer.Core.Common;

namespace PetIdServer.Core.Entities;

public class Tag : Entity<int>
{
    public Pet? Pet { get; private set; }

    public bool IsAlreadyInUse => Pet is not null;

    public Tag(int id) : base(id)
    {
    }

    public void SetupPet(Pet pet)
    {
        if (IsAlreadyInUse) throw new Exception(); // TAG_IS_ALREADY_IN_USE
        
        Pet = pet;
    }
}