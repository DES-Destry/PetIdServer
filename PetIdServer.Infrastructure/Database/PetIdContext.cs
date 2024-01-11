using Microsoft.EntityFrameworkCore;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database;

public class PetIdContext : DbContext
{
    public PetIdContext(DbContextOptions<PetIdContext> options) : base(options) { }
    public DbSet<OwnerModel> Owners { get; set; }
    public DbSet<OwnerContactModel> OwnerContacts { get; set; }
    public DbSet<PetModel> Pets { get; set; }
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<TagReportModel> TagReports { get; set; }

    public DbSet<AdminModel> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("pet");

        modelBuilder.Entity<OwnerModel>().Navigation(owner => owner.Pets).AutoInclude();
        modelBuilder.Entity<OwnerModel>().Navigation(owner => owner.Contacts).AutoInclude();

        modelBuilder.Entity<TagModel>().Navigation(tag => tag.Pet).AutoInclude();

        modelBuilder
            .Entity<TagReportModel>()
            .Navigation(report => report.CorruptedTag)
            .AutoInclude();
        modelBuilder.Entity<TagReportModel>().Navigation(report => report.Reporter).AutoInclude();
        modelBuilder.Entity<TagReportModel>().Navigation(report => report.Resolver).AutoInclude();

        modelBuilder.Entity<AdminModel>().HasData(new AdminModel
        {
            Username = "Andrey.Kirik",
            Password = null,
            CreatedAt = DateTime.UtcNow,
            PasswordLastChangedAt = null
        });

        base.OnModelCreating(modelBuilder);
    }
}
