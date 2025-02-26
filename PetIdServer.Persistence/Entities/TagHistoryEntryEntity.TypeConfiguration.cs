using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Persistence.Entities;

public class TagHistoryEntryEntityTypeConfiguration : IEntityTypeConfiguration<TagHistoryEntryEntity>
{
    public void Configure(EntityTypeBuilder<TagHistoryEntryEntity> builder)
    {
        builder.ToTable("tag_history").HasKey(e => e.Id);

        builder.HasOne(e => e.RelatedTag)
            .WithMany(t => t.History)
            .HasForeignKey(e => e.TagId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Initiator)
            .WithMany(u => u.TagActions)
            .HasForeignKey(e => e.InitiatorId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(e => e.TagId)
            .HasColumnName("tag_id")
            .IsRequired();

        builder.Property(e => e.InitiatorId)
            .HasColumnName("initiator_id")
            .IsRequired(false);

        builder.Property(e => e.StatusFrom)
            .HasColumnName("status_from")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(e => e.StatusTo)
            .HasColumnName("status_to")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(e => e.ChangedAt)
            .HasColumnName("changed_at")
            .IsRequired();
    }
}
