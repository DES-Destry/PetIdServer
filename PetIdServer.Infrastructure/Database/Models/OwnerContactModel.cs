using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetIdServer.Infrastructure.Database.Models;

[Table("owners_contacts")]
public class OwnerContactModel
{
    [Column("owner_id"), Required, Key] public string OwnerId { get; set; }

    [Column("contact_type"), Required, Key]
    public string ContactType { get; set; }

    [Column("contact"), Required] public string Contact { get; set; }
    
    
    [ForeignKey("OwnerId")] public virtual OwnerModel Owner { get; set; }
}