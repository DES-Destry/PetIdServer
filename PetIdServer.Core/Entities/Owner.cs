using PetIdServer.Core.Common;
using PetIdServer.Core.ValueObjects;

namespace PetIdServer.Core.Entities;

public class Owner : Entity<string>
{
    public string Email => Id;
    /// <summary>
    /// Storing only as hash
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

    public Owner(CreationAttributes creationAttributes) : base(creationAttributes.Email)
    {
        Password = creationAttributes.Password;
        Name = creationAttributes.Name;
    }

    public Owner(string id) : base(id)
    {
    }
}