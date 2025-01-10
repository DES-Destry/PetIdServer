using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetIdServer.Infrastructure.Database.Domain.Pet;

namespace PetIdServer.Infrastructure.Database.Domain.User;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("users").HasKey(user => user.Id);

        builder.HasMany(user => user.Contacts)
            .WithOne(contact => contact.User)
            .HasForeignKey(contact => contact.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<PetModel>(user => user.Pets)
            .WithOne(pet => pet.User)
            .HasForeignKey(pet => pet.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);

        builder.OwnsMany<PetModel>(user => user.Pets);

        builder.Navigation(user => user.Contacts).AutoInclude();
        builder.Navigation(user => user.Pets).AutoInclude();

        builder.Property(user => user.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();

        builder.Property(user => user.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(user => user.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(320);

        builder.Property(user => user.Role)
            .HasColumnName("role")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(user => user.Password)
            .HasColumnName("password")
            .HasMaxLength(1024);

        builder.Property(user => user.Address)
            .HasColumnName("address")
            .HasMaxLength(256);

        builder.Property(user => user.Description)
            .HasColumnName("description")
            .HasMaxLength(4096);
    }
}
