using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetIdServer.Persistence.Entities;

public class TagReportEntityTypeConfiguration : IEntityTypeConfiguration<TagReportEntity>
{
    public void Configure(EntityTypeBuilder<TagReportEntity> builder)
    {
        builder.ToTable("tag_reports").HasKey(report => report.Id);

        builder.HasOne(report => report.CorruptedTag)
            .WithMany(tag => tag.Reports)
            .HasForeignKey(report => report.CorruptedTagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(report => report.Reporter)
            .WithMany(admin => admin.TagReportsCreated)
            .HasForeignKey(report => report.ReporterId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(report => report.Resolver)
            .WithMany(admin => admin.TagReportsResolved)
            .HasForeignKey(report => report.ResolverId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(report => report.CorruptedTag).AutoInclude();
        builder.Navigation(report => report.Reporter).AutoInclude();
        builder.Navigation(report => report.Resolver).AutoInclude();

        builder.HasOne<UserEntity>();

        builder.Property(report => report.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(report => report.CorruptedTagId)
            .HasColumnName("corrupted_tag_id")
            .IsRequired();

        builder.Property(report => report.ReporterId)
            .HasColumnName("reporter_id")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(report => report.ResolverId)
            .HasColumnName("resolver_id")
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(report => report.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(report => report.ResolvedAt)
            .HasColumnName("resolved_at");
    }
}
