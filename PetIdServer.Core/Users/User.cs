using PetIdServer.Core.Common;
using PetIdServer.Core.Pets;

namespace PetIdServer.Core.Users;

public class User : Entity<UserId>
{
    private User() : base((UserId)Guid.NewGuid()) { }
    public required string Email { get; init; }

    public PasswordHash? Password { get; private set; }

    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public required UserRole Role { get; init; }
    public IList<UserContact> Contacts { get; set; } = [];
    public IList<Pet> Pets { get; private set; } = [];

    public static User CreateNew(CreationAttributes creationAttributes)
    {
        return new User
        {
            Email = creationAttributes.Email,
            Password = creationAttributes.Password,
            Name = creationAttributes.Name,
            Role = creationAttributes.Role ?? UserRole.LeastPrivileged
        };
    }

    public static User CreateFromPersistence(UserId id,
        string email,
        PasswordHash? password,
        string name,
        string? address,
        string? description,
        UserRole role,
        IList<UserContact> contacts,
        IList<Pet> pets)
    {
        return new User
        {
            Id = id,
            Email = email,
            Password = password,
            Name = name,
            Address = address,
            Description = description,
            Role = role,
            Contacts = contacts,
            Pets = pets
        };
    }

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
