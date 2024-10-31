using Microsoft.EntityFrameworkCore;
using PetIdServer.Infrastructure.Database.Domain.Admin;
using PetIdServer.Infrastructure.Database.Domain.Owner;
using PetIdServer.Infrastructure.Database.Domain.Pet;
using PetIdServer.Infrastructure.Database.Domain.Tag;

namespace PetIdServer.Infrastructure.Database;

public class PetIdContext(DbContextOptions<PetIdContext> options) : DbContext(options)
{
    public DbSet<OwnerModel> Owners { get; init; }
    public DbSet<OwnerContactModel> OwnerContacts { get; init; }
    public DbSet<PetModel> Pets { get; init; }
    public DbSet<TagModel> Tags { get; init; }
    public DbSet<TagReportModel> TagReports { get; init; }

    public DbSet<AdminModel> Admins { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("pet");

        new AdminEntityTypeConfiguration().Configure(modelBuilder.Entity<AdminModel>());

        new OwnerEntityTypeConfiguration().Configure(modelBuilder.Entity<OwnerModel>());
        new OwnerContactEntityTypeConfiguration().Configure(modelBuilder.Entity<OwnerContactModel>());

        new PetEntityTypeConfiguration().Configure(modelBuilder.Entity<PetModel>());

        new TagEntityTypeConfiguration().Configure(modelBuilder.Entity<TagModel>());
        new TagReportEntityTypeConfiguration().Configure(modelBuilder.Entity<TagReportModel>());

        base.OnModelCreating(modelBuilder);
    }
}
