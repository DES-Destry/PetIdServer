using PetIdServer.Core.Common;
using PetIdServer.Core.ValueObjects;

namespace PetIdServer.Core.Entities;

public class Owner : Entity<Guid>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public IList<OwnerContact> Contacts { get; set; } = new List<OwnerContact>();

    public record CreationAttributes(
        Guid Id,
        string Email,
        string Password,
        string Name
    );

    public Owner(CreationAttributes creationAttributes) : base(creationAttributes.Id)
    {
        Email = creationAttributes.Email;
        Password = creationAttributes.Password;
        Name = creationAttributes.Name;
    }

    public Owner(Guid id) : base(id)
    {
    }
}