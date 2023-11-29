using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetIdServer.Infrastructure.Database.Models;

[Table("tags")]
public class TagModel
{
    [Column("id")] [Required] [Key] public int Id { get; set; }

    [Column("code")] [Required] public string Code { get; set; }

    [Column("control_code")] [Required] public long ControlCode { get; set; }

    [Column("pet_id")] public Guid? PetId { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }

    [Column("pet_added_at")] public DateTime PetAddedAt { get; set; }

    [Column("last_scanned_at")] public DateTime LastScannedAt { get; set; }


    [ForeignKey("PetId")] public virtual PetModel? Pet { get; set; }
}