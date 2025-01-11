using PetIdServer.Core.Common;
using PetIdServer.Core.Pets;

namespace PetIdServer.Core.Users;

public class User : Entity<UserId>
{
    public User(CreationAttributes creationAttributes) : base((UserId)Guid.NewGuid())
    {
        Email = creationAttributes.Email;
        Password = creationAttributes.Password;
        Name = creationAttributes.Name;
        Role = creationAttributes.Role ?? UserRole.LeastPrivileged;
    }

    public User(UserId id) : base(id) { }

    public string Email { get; init; }

    /// <summary>
    ///     Storing only as a hash
    /// </summary>
    public PasswordHash? Password { get; private set; }

    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public UserRole Role { get; init; }
    public IList<UserContact> Contacts { get; set; } = [];
    public IList<Pet> Pets { get; set; } = [];

    public void ChangePasswordHash(PasswordHash passwordHash)
    {
        Password = passwordHash;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeAddress(string address)
    {
        Address = address;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }

    public void RemoveContactWithType(string type)
    {
        Contacts = Contacts
            .Where(contact => contact.ContactType != type)
            .ToList();
    }

    public record CreationAttributes(
        string Email,
        PasswordHash Password,
        string Name,
        UserRole? Role = null
    );
}
