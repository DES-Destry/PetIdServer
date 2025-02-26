using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Persistence.Entities;

public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("user_roles").HasKey(x => new
        {
            x.UserId, x.Role
        });

        builder.HasOne(x => x.User)
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Role)
            .HasColumnName("role")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();
    }
}
