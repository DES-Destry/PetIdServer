using PetIdServer.Core.Common;
using PetIdServer.Core.Domain.Owner;

namespace PetIdServer.Core.Domain.Pet;

public class PetEntity : Entity<PetId>
{
    public PetEntity(CreationAttributes creationAttributes) : base(creationAttributes.Id)
    {
        Type = creationAttributes.Type;
        Name = creationAttributes.Name;
        Sex = creationAttributes.Sex;
        IsCastrated = creationAttributes.IsCastrated;
    }

    // Mapper require this constructor
    public PetEntity(PetId id) : base(id) { }

    public OwnerEntity Owner { get; set; }
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
