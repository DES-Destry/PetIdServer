using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetIdServer.Infrastructure.Database.Models;

[Table("admins")]
public class AdminModel
{
    [Column("username"), Required, Key, MaxLength(32)]
    public string Username { get; set; }

    [Column("password")] public string? Password { get; set; }

    [Column("created_at"), Required] public DateTime CreatedAt { get; set; }

    [Column("password_last_changed_at")] public DateTime? PasswordLastChangedAt { get; set; }
}