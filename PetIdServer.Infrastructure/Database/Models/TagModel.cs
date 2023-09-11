using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetIdServer.Infrastructure.Database.Models;

[Table("tags")]
public class TagModel
{
    [Column("id"), Required, Key]
    public int Id { get; set; }

    [Column("security_code"), Required]
    public string SecurityCode { get; set; }

    [Column("pet_id")]
    public Guid? PetId { get; set; }
    
    
    [ForeignKey("PetId")] public virtual PetModel? Pet { get; set; }
}