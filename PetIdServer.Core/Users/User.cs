using PetIdServer.Core.Common;
using PetIdServer.Core.Pets;

namespace PetIdServer.Core.Users;

public class User(User.CreationAttributes creationAttributes) : Entity<UserId>((UserId)Guid.NewGuid())
{
    public string Email { get; init; } = creationAttributes.Email;

    /// <summary>
    ///     Storing only as a hash
    /// </summary>
    public PasswordHash? Password { get; private set; } = creationAttributes.Password;

    public string Name { get; set; } = creationAttributes.Name;
    public string? Address { get; set; }
    public string? Description { get; set; }
    public UserRole Role { get; init; } = creationAttributes.Role ?? UserRole.LeastPrivileged;
    public IList<UserContact> Contacts { get; set; } = [];
    public IList<Pet> Pets { get; private set; } = [];

    public void Update(UpdateAttributes updateAttributes)
    {
        Name = updateAttributes.Name ?? Name;
        Address = updateAttributes.Address ?? Address;
        Description = updateAttributes.Description ?? Description;
    }

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

    public record UpdateAttributes
    {
        public string? Name { get; init; }
        public string? Address { get; init; }
        public string? Description { get; init; }
    }
}
