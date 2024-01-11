using PetIdServer.Core.Common;

namespace PetIdServer.Core.Domain.Owner;

public class OwnerContactVo : ValueObject
{
    public string ContactType { get; set; } = "_default";
    public string Contact { get; set; } = "_default";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return nameof(GetType);
        yield return nameof(ContactType);
        yield return ContactType;
        yield return nameof(Contact);
        yield return Contact;
    }
}
