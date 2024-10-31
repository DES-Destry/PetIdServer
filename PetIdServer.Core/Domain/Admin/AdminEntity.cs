using PetIdServer.Core.Common;

namespace PetIdServer.Core.Domain.Admin;

public class AdminEntity : Entity<AdminId>
{
    public AdminEntity(CreationAttributes creationAttributes) : base(
        (AdminId)creationAttributes.Username)
    {
        Password = creationAttributes.Password;

        CreatedAt = DateTime.UtcNow;
        PasswordMustBeChangedBefore = DateTime.UtcNow.AddHours(12);
        PasswordLastChangedAt = null;
    }

    public AdminEntity(AdminId id) : base(id) { }

    public string Username => Id;

    /// <summary>
    ///     Storing only as a hash or empty value
    /// </summary>
    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PasswordMustBeChangedBefore { get; set; }

    public DateTime? PasswordLastChangedAt { get; set; }

    public bool CanDoActions => Password is not null;

    public bool MustBeDeleted => DateTime.UtcNow.CompareTo(PasswordMustBeChangedBefore) == 1;

    public record EntityId(string Value);

    public record CreationAttributes(string Username, string Password);
}
