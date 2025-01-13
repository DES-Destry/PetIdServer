using PetIdServer.Core.Common;
using PetIdServer.Core.Users;

namespace PetIdServer.Core.Pets;

public class Pet(Pet.CreationAttributes creationAttributes) : Entity<PetId>((PetId)Guid.NewGuid())
{
    public User? User { get; private set; }

    public string Type { get; private set; } = creationAttributes.Type;

    public string Name { get; private set; } = creationAttributes.Name;
    public bool Sex { get; private set; } = creationAttributes.Sex;
    public bool IsCastrated { get; private set; } = creationAttributes.IsCastrated;
    public string? Photo { get; private set; }
    public string? Description { get; private set; }

    public void Update(UpdateAttributes updateAttributes)
    {
        Type = updateAttributes.Type ?? Type;
        Name = updateAttributes.Name ?? Name;
        Sex = updateAttributes.Sex ?? Sex;
        IsCastrated = updateAttributes.IsCastrated ?? IsCastrated;
        Photo = updateAttributes.Photo ?? Photo;
        Description = updateAttributes.Description ?? Description;
    }

    public record CreationAttributes(
        string Type,
        string Name,
        bool Sex,
        bool IsCastrated
    );

    public record UpdateAttributes
    {
        public string? Type { get; init; }
        public string? Name { get; init; }
        public bool? Sex { get; init; }
        public bool? IsCastrated { get; init; }
        public string? Photo { get; init; }
        public string? Description { get; init; }
    }
}
