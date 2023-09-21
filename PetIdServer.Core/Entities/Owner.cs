using PetIdServer.Core.Common;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Core.ValueObjects;

namespace PetIdServer.Core.Entities;

public class Owner : Entity<OwnerId>
{
    public string Email => Id.Value;
    /// <summary>
    /// Storing only as a hash
    /// </summary>
    public string Password { get; set; }

    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public IList<OwnerContact> Contacts { get; set; } = new List<OwnerContact>();
    public IList<Pet> Pets { get; set; } = new List<Pet>();

    public record CreationAttributes(
        string Email,
        string Password,
        string Name
    );

    public Owner(CreationAttributes creationAttributes) : base(new OwnerId(creationAttributes.Email))
    {
        Password = creationAttributes.Password;
        Name = creationAttributes.Name;
    }

    public Owner(OwnerId id) : base(id)
    {
    }
}