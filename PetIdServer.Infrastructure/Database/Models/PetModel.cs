using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetIdServer.Infrastructure.Database.Models;

[Table("pets")]
public class PetModel
{
    [Column("id"), Required, Key] public Guid Id { get; set; }

    [Column("owner_id"), Required] public string OwnerId { get; set; }

    [Column("type"), Required, MaxLength(16)]
    public string Type { get; set; }

    [Column("name"), Required, MaxLength(32)]
    public string Name { get; set; }

    [Column("sex"), Required] public bool Sex { get; set; }

    [Column("is_castrated"), Required] public bool IsCastrated { get; set; }

    [Column("photo")] public string? Photo { get; set; }

    [Column("description"), MaxLength(4096)]
    public string? Description { get; set; }

    
    [ForeignKey("OwnerId")] public virtual OwnerModel Owner { get; set; }
    public virtual ICollection<TagModel> Tags { get; set; }
}