using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Persistence.Entities;

public class TagFeatureEntityTypeConfiguration : IEntityTypeConfiguration<TagFeatureEntity>
{
    public void Configure(EntityTypeBuilder<TagFeatureEntity> builder)
    {
        builder.ToTable("tag_features").HasKey(e => new
        {
            e.TagId, e.Feature
        });

        builder.HasOne(feature => feature.Tag)
            .WithMany(tag => tag.Features)
            .HasForeignKey(feature => feature.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(feature => feature.TagId)
            .HasColumnName("tag_id")
            .IsRequired();

        builder.Property(feature => feature.Feature)
            .HasColumnName("feature")
            .IsRequired()
            .HasMaxLength(32);
    }
}
