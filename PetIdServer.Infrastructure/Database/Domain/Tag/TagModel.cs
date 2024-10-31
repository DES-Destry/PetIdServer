using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetIdServer.Infrastructure.Database.Domain.Pet;

namespace PetIdServer.Infrastructure.Database.Domain.Tag;

[Table("tags")]
public class TagModel
{
    [Column("id")][Required][Key] public int Id { get; init; }

    [Column("code")][MaxLength(1024)][Required]
    public required string Code { get; init; }

    [Column("hash_code")][MaxLength(100)][Required]
    public required string HashCode { get; init; }

    [Column("control_code")][Required] public long ControlCode { get; init; }

    [Column("pet_id")] public Guid? PetId { get; init; }

    [Column("created_at")][Required] public DateTime CreatedAt { get; init; }

    [Column("pet_added_at")] public DateTime? PetAddedAt { get; init; }

    [Column("last_scanned_at")] public DateTime? LastScannedAt { get; init; }

    [ForeignKey("PetId")] public virtual PetModel? Pet { get; set; }
}
