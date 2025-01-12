using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Persistence.Entities;

public class UserContactEntityTypeConfiguration : IEntityTypeConfiguration<UserContactEntity>
{
    public void Configure(EntityTypeBuilder<UserContactEntity> builder)
    {
        builder.ToTable("user_contacts");
        builder.HasKey(contact => new
        {
            contact.UserId, contact.ContactType
        });

        builder.HasOne<UserEntity>(contact => contact.User)
            .WithMany(user => user.Contacts)
            .HasForeignKey(contact => contact.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(contact => contact.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(contact => contact.ContactType)
            .HasColumnName("contact_type")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(contact => contact.Contact)
            .HasColumnName("contact")
            .IsRequired()
            .HasMaxLength(128);
    }
}
