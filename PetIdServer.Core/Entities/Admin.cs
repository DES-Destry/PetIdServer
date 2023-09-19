using PetIdServer.Core.Common;

namespace PetIdServer.Core.Entities;

public class Admin : Entity<Guid>
{
    public string Username { get; set; }

    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PasswordLastChangedAt { get; set; }

    public record CreationAttributes(Guid Id, string Username, string Password);
    
    public Admin(CreationAttributes creationAttributes) : base(creationAttributes.Id)
    {
        Username = creationAttributes.Username;
        Password = creationAttributes.Password;
        
        CreatedAt = DateTime.UtcNow;
        PasswordLastChangedAt = DateTime.UtcNow;
    }

    public Admin(Guid id) : base(id)
    {
    }
}