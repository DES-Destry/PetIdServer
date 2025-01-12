using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Persistence.Entities;

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<TagEntity>
{
    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.ToTable("tags").HasKey(tag => tag.Id);

        builder.HasOne<PetEntity>()
            .WithMany(pet => pet.Tags)
            .HasForeignKey(tag => tag.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(tag => tag.Pet).AutoInclude();

        builder.Property(tag => tag.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(tag => tag.Code)
            .HasColumnName("code")
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(tag => tag.HashCode)
            .HasColumnName("hash_code")
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(tag => tag.ControlCode)
            .HasColumnName("control_code")
            .IsRequired();

        builder.Property(tag => tag.PetId)
            .HasColumnName("pet_id");

        builder.Property(tag => tag.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder.Property(tag => tag.PetAddedAt)
            .HasColumnName("pet_added_at");

        builder.Property(tag => tag.LastScannedAt)
            .HasColumnName("last_scanned_at");
    }
}
