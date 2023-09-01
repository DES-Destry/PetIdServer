using PetIdServer.Core.Common;

namespace PetIdServer.Core.Entities;

public class Tag : Entity<int>
{
    public Pet Pet { get; private set; }

    public Tag(int id) : base(id)
    {
    }

    public void SetupPet(Pet pet)
    {
        if (Pet is not null) throw new Exception(); // TAG_IS_ALREADY_IN_USE
        
        Pet = pet;
    }
}