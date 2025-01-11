using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Infrastructure.Database.Entities;

public class PetEntityTypeConfiguration : IEntityTypeConfiguration<PetModel>
{
    public void Configure(EntityTypeBuilder<PetModel> builder)
    {
        builder.ToTable("pets").HasKey(pet => pet.Id);

        builder.HasMany<TagModel>(pet => pet.Tags)
            .WithOne(tag => tag.Pet)
            .HasForeignKey(tag => tag.PetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<UserModel>()
            .WithMany(user => user.Pets)
            .HasForeignKey(user => user.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(pet => pet.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();

        builder.Property(pet => pet.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(pet => pet.Type)
            .HasColumnName("type")
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(pet => pet.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(pet => pet.Sex)
            .HasColumnName("sex")
            .IsRequired();

        builder.Property(pet => pet.IsCastrated)
            .HasColumnName("is_castrated")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(pet => pet.PhotoId)
            .HasColumnName("photo_id")
            .IsRequired();

        builder.Property(pet => pet.Description)
            .HasColumnName("description")
            .HasMaxLength(4096);
    }
}
