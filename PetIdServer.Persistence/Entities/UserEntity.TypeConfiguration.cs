using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Persistence.Entities;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users").HasKey(user => user.Id);

        builder.HasMany(user => user.Contacts)
            .WithOne(contact => contact.User)
            .HasForeignKey(contact => contact.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.Pets)
            .WithOne(pet => pet.User)
            .HasForeignKey(pet => pet.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(user => user.TagReportsCreated)
            .WithOne(report => report.Reporter)
            .HasForeignKey(report => report.Reporter)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(user => user.TagReportsResolved)
            .WithOne(report => report.Resolver)
            .HasForeignKey(report => report.Resolver)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(user => user.TagActions)
            .WithOne(action => action.Initiator)
            .HasForeignKey(action => action.InitiatorId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

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
