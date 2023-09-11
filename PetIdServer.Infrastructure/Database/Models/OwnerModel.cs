using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetIdServer.Infrastructure.Database.Models;

[Table("owners")]
public class OwnerModel
{
    // Owner's email
    [Column("id"), Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Column("password"), Required] public string Password { get; set; }

    [Column("name"), Required, MaxLength(32)]
    public string Name { get; set; }

    [Column("address")] public string? Address { get; set; }

    [Column("description"), MaxLength(4096)]
    public string? Description { get; set; }

    public virtual ICollection<OwnerContactModel> Contacts { get; set; }
    public virtual ICollection<PetModel> Pets { get; set; }
}