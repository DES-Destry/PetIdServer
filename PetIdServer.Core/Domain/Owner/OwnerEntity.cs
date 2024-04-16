using PetIdServer.Core.Common;
using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Core.Domain.Owner;

public class OwnerEntity : Entity<OwnerId>
{
    public OwnerEntity(CreationAttributes creationAttributes) : base((OwnerId) Guid.NewGuid())
    {
        Email = creationAttributes.Email;
        Password = creationAttributes.Password;
        Name = creationAttributes.Name;
    }

    public OwnerEntity(OwnerId id) : base(id) { }

    public string Email { get; set; }

    /// <summary>
    ///     Storing only as a hash
    /// </summary>
    public string Password { get; set; }

    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public IList<OwnerContactVo> Contacts { get; set; } = new List<OwnerContactVo>();
    public IList<PetEntity> Pets { get; set; } = new List<PetEntity>();

    public record CreationAttributes(
        string Email,
        string Password,
        string Name
    );
}
