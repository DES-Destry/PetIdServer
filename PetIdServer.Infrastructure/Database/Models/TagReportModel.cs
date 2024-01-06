using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetIdServer.Infrastructure.Database.Models;

[Table("tag_reports")]
public class TagReportModel
{
    [Column("id")] [Required] [Key] public Guid Id { get; set; }

    [Column("corrupted_tag_id")]
    [Required]
    public int CorruptedTagId { get; set; }

    [Column("reporter_id")] [Required] public string ReporterId { get; set; }
    [Column("resolver_id")] public string? ResolverId { get; set; }
    [Column("created_at")] [Required] public DateTime CreatedAt { get; set; }
    [Column("resolved_at")] public DateTime? ResolvedAt { get; set; }

    [ForeignKey(nameof(CorruptedTagId))] public virtual TagModel CorruptedTag { get; set; }
    [ForeignKey(nameof(ReporterId))] public virtual AdminModel Reporter { get; set; }
    [ForeignKey(nameof(ResolverId))] public virtual AdminModel? Resolver { get; set; }

    public bool IsResolved => Resolver is not null;
}
