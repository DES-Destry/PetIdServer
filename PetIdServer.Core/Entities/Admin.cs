using PetIdServer.Core.Common;
using PetIdServer.Core.Entities.Id;

namespace PetIdServer.Core.Entities;

public class Admin : Entity<AdminId>
{
    public string Username => Id.Value;

    /// <summary>
    /// Storing only as a hash or empty value
    /// </summary>
    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PasswordLastChangedAt { get; set; }

    public bool IsNotCapable => Password is null;

    public record EntityId(string Value);
    public record CreationAttributes(string Username, string Password);
    
    public Admin(CreationAttributes creationAttributes) : base(new AdminId(creationAttributes.Username))
    {
        Password = creationAttributes.Password;
        
        CreatedAt = DateTime.UtcNow;
        PasswordLastChangedAt = DateTime.UtcNow;
    }

    public Admin(AdminId id) : base(id)
    {
    }
}