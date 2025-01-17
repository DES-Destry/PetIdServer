using System.Diagnostics.CodeAnalysis;
using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class UserContactMapper
{
    [return: NotNullIfNotNull("entity")]
    public static UserContact? ToCore(this UserContactEntity? entity) => entity is null
        ? null
        : new UserContact
        {
            ContactType = entity.ContactType, Contact = entity.Contact
        };

    public static UserContactEntity ToEntity(this UserContact? contact, UserId? contactOwnerId)
    {
        ArgumentNullException.ThrowIfNull(contact);
        ArgumentNullException.ThrowIfNull(contactOwnerId);

        return new UserContactEntity
        {
            UserId = (Guid)contactOwnerId, ContactType = contact.ContactType, Contact = contact.Contact
        };
    }
}
