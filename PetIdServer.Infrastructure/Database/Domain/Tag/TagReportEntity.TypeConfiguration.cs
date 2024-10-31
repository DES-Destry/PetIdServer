using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetIdServer.Infrastructure.Database.Domain.Admin;

namespace PetIdServer.Infrastructure.Database.Domain.Tag;

public class TagReportEntityTypeConfiguration : IEntityTypeConfiguration<TagReportModel>
{
    public void Configure(EntityTypeBuilder<TagReportModel> builder)
    {
        builder.ToTable("tag_reports").HasKey(report => report.Id);

        builder.HasOne<TagModel>()
            .WithMany(tag => tag.Reports)
            .HasForeignKey(report => report.CorruptedTagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<AdminModel>()
            .WithMany(admin => admin.TagReportsCreated)
            .HasForeignKey(report => report.ReporterId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<AdminModel>()
            .WithMany(admin => admin.TagReportsResolved)
            .HasForeignKey(report => report.ResolverId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(report => report.CorruptedTag).AutoInclude();
        builder.Navigation(report => report.Reporter).AutoInclude();
        builder.Navigation(report => report.Resolver).AutoInclude();

        builder.HasOne<AdminModel>();

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
