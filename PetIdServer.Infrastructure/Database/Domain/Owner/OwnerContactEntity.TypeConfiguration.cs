using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Infrastructure.Database.Domain.Owner;

public class OwnerContactEntityTypeConfiguration : IEntityTypeConfiguration<OwnerContactModel>
{
    public void Configure(EntityTypeBuilder<OwnerContactModel> builder)
    {
        builder.ToTable("owners_contacts");
        builder.HasKey(contact => new
        {
            contact.OwnerId, contact.ContactType
        });

        builder.HasOne<OwnerModel>(contact => contact.Owner)
            .WithMany(owner => owner.Contacts)
            .HasForeignKey(contact => contact.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(contact => contact.OwnerId)
            .HasColumnName("owner_id")
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
