using PetIdServer.Core.Common;

namespace PetIdServer.Core.Domains.Pet;

public class Pet : Entity<PetId>
{
    public Pet(CreationAttributes creationAttributes) : base(creationAttributes.Id)
    {
        Type = creationAttributes.Type;
        Name = creationAttributes.Name;
        Sex = creationAttributes.Sex;
        IsCastrated = creationAttributes.IsCastrated;
    }

    // Mapper require this constructor
    public Pet(PetId id) : base(id)
    {
    }

    public Owner.Owner Owner { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public bool Sex { get; set; }
    public bool IsCastrated { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }

    public record CreationAttributes(
        PetId Id,
        string Type,
        string Name,
        bool Sex,
        bool IsCastrated
    );
}