using PetIdServer.Core.Common;

namespace PetIdServer.Core.Entities;

public class Pet : Entity<Guid>
{
    public Owner Owner { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public bool Sex { get; set; }
    public bool IsCastrated { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }

    public record CreationAttributes(
        Guid id,
        string Type,
        string Name,
        bool Sex,
        bool IsCastrated
    );
    
    public Pet(CreationAttributes creationAttributes) : base(creationAttributes.id)
    {
        Type = creationAttributes.Type;
        Name = creationAttributes.Name;
        Sex = creationAttributes.Sex;
        IsCastrated = creationAttributes.IsCastrated;
    }

    // Mapper require this constructor
    public Pet(Guid id) : base(id)
    {
    }
}