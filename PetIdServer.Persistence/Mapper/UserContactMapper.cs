using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class UserContactMapper
{
    public static UserContact ToCore(this UserContactEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new UserContact
        {
            ContactType = entity.ContactType, Contact = entity.Contact
        };
    }

    public static UserContactEntity ToEntity(this UserContact? contact, User? contactOwner)
    {
        ArgumentNullException.ThrowIfNull(contact);
        ArgumentNullException.ThrowIfNull(contactOwner);

        return new UserContactEntity
        {
            UserId = contactOwner.Id, ContactType = contact.ContactType, Contact = contact.Contact
        };
    }
}
