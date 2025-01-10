using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Infrastructure.Database.Domain.User;

public class UserContactEntityTypeConfiguration : IEntityTypeConfiguration<UserContactModel>
{
    public void Configure(EntityTypeBuilder<UserContactModel> builder)
    {
        builder.ToTable("user_contacts");
        builder.HasKey(contact => new
        {
            contact.UserId, contact.ContactType
        });

        builder.HasOne<UserModel>(contact => contact.User)
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
