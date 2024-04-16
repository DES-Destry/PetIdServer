using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetIdServer.Infrastructure.Database.Models;

[Table("owners_contacts")]
[PrimaryKey(nameof(OwnerId), nameof(ContactType))]
public class OwnerContactModel
{
    [Column("owner_id")] [Required] [Key] public Guid OwnerId { get; set; }

    [Column("contact_type")]
    [Required]
    [Key]
    public string ContactType { get; set; }

    [Column("contact")] [Required] public string Contact { get; set; }


    [ForeignKey("OwnerId")] public virtual OwnerModel Owner { get; set; }
}
