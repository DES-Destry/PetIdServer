using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetIdServer.Infrastructure.Database.Domain.Pet;

namespace PetIdServer.Infrastructure.Database.Domain.Owner;

public class OwnerEntityTypeConfiguration : IEntityTypeConfiguration<OwnerModel>
{
    public void Configure(EntityTypeBuilder<OwnerModel> builder)
    {
        builder.ToTable("owners").HasKey(owner => owner.Id);

        builder.HasMany(owner => owner.Contacts)
            .WithOne(contact => contact.Owner)
            .HasForeignKey(contact => contact.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<PetModel>(owner => owner.Pets)
            .WithOne(pet => pet.Owner)
            .HasForeignKey(pet => pet.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);

        builder.OwnsMany<PetModel>(owner => owner.Pets);

        builder.Navigation(owner => owner.Contacts).AutoInclude();
        builder.Navigation(owner => owner.Pets).AutoInclude();

        builder.Property(owner => owner.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();

        builder.Property(owner => owner.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(owner => owner.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(320);

        builder.Property(owner => owner.Password)
            .HasColumnName("password")
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(owner => owner.Address)
            .HasColumnName("address")
            .HasMaxLength(256);

        builder.Property(owner => owner.Description)
            .HasColumnName("description")
            .HasMaxLength(4096);
    }
}
