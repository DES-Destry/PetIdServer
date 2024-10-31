using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Infrastructure.Database.Domain.Admin;

public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<AdminModel>
{
    public void Configure(EntityTypeBuilder<AdminModel> builder)
    {
        builder.ToTable("admins").HasKey(admin => admin.Username);

        builder.Property(admin => admin.Username)
            .HasColumnName("username")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(admin => admin.Password)
            .HasColumnName("password")
            .HasMaxLength(1024);

        builder.Property(admin => admin.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(admin => admin.PasswordLastChangedAt)
            .HasColumnName("password_last_changed_at");

        builder.HasData(new AdminModel
        {
            Username = "Andrey.Kirik", Password = null, CreatedAt = DateTime.UtcNow, PasswordLastChangedAt = null
        });
    }
}
