using Microsoft.EntityFrameworkCore;
using PetIdServer.Infrastructure.Database.Entities;

namespace PetIdServer.Infrastructure.Database;

public class PetIdContext(DbContextOptions<PetIdContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; init; }
    public DbSet<UserContactModel> UserContacts { get; init; }
    public DbSet<PetModel> Pets { get; init; }
    public DbSet<TagModel> Tags { get; init; }
    public DbSet<TagReportModel> TagReports { get; init; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("pet");

        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<UserModel>());
        new UserContactEntityTypeConfiguration().Configure(modelBuilder.Entity<UserContactModel>());

        new PetEntityTypeConfiguration().Configure(modelBuilder.Entity<PetModel>());

        new TagEntityTypeConfiguration().Configure(modelBuilder.Entity<TagModel>());
        new TagReportEntityTypeConfiguration().Configure(modelBuilder.Entity<TagReportModel>());

        base.OnModelCreating(modelBuilder);
    }
}
