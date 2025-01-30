using PetIdServer.Core.Common;

namespace PetIdServer.Core.Users;

public class User : AggregateRoot<UserId>
{
    private Dictionary<string, string> _contacts = [];

    private User(string name) : base((UserId)Guid.NewGuid())
    {
        Name = name;
    }

    public required string Email { get; init; }

    public PasswordHash? Password { get; private set; }

    public string Name { get; private set; }
    public string? Address { get; private set; }
    public string? Description { get; private set; }
    public required UserRole Role { get; init; }

    public IReadOnlyList<UserContact> Contacts => _contacts.Select(contact => new UserContact
    {
        ContactType = contact.Key,
        Contact = contact.Value
    }).ToArray();

    public static User CreateNew(CreationAttributes creationAttributes)
    {
        return new User(creationAttributes.Name)
        {
            Email = creationAttributes.Email,
            Password = creationAttributes.Password,
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
        IList<UserContact> contacts)
    {
        return new User(name)
        {
            Id = id,
            Email = email,
            Password = password,
            Address = address,
            Description = description,
            Role = role,
            _contacts = contacts.ToDictionary(contact => contact.ContactType, contact => contact.Contact)
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

    public void AddContact(string contactType, string contact)
    {
        _contacts.Add(contactType, contact);
    }

    public void RemoveContactWithType(string type)
    {
        _contacts.Remove(type);
    }

    public bool HasPermissionsOf(UserRole role) => role.HasPermissionsOf(Role);

    public record CreationAttributes(
        string Email,
        string Name,
        PasswordHash? Password = null,
        UserRole? Role = null
    );

    public record UpdateAttributes
    {
        public string? Name { get; init; }
        public string? Address { get; init; }
        public string? Description { get; init; }
    }
}
