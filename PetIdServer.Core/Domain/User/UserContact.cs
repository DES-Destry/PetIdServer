using PetIdServer.Core.Common;

namespace PetIdServer.Core.Domain.User;

public class UserContact : ValueObject
{
    public required string ContactType { get; init; }
    public required string Contact { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return nameof(GetType);
        yield return nameof(ContactType);
        yield return ContactType;
        yield return nameof(Contact);
        yield return Contact;
    }
}
