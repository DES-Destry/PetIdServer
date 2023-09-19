using PetIdServer.Core.Common;

namespace PetIdServer.Core.Entities;

public class Admin : Entity<string>
{
    public string Username => Id;

    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PasswordLastChangedAt { get; set; }

    public bool IsNotCapable => Password is null;

    public record CreationAttributes(string Username, string Password);
    
    public Admin(CreationAttributes creationAttributes) : base(creationAttributes.Username)
    {
        Password = creationAttributes.Password;
        
        CreatedAt = DateTime.UtcNow;
        PasswordLastChangedAt = DateTime.UtcNow;
    }

    public Admin(string id) : base(id)
    {
    }
}