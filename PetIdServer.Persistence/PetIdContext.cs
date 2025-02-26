using Microsoft.EntityFrameworkCore;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence;

public class PetIdContext(DbContextOptions<PetIdContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; init; }
    public DbSet<UserContactEntity> UserContacts { get; init; }
    public DbSet<PetEntity> Pets { get; init; }
    public DbSet<TagEntity> Tags { get; init; }
    public DbSet<TagReportEntity> TagReports { get; init; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("pet");
        modelBuilder.HasPostgresExtension("uuid-ossp");

        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<UserEntity>());
        new UserContactEntityTypeConfiguration().Configure(modelBuilder.Entity<UserContactEntity>());

        new PetEntityTypeConfiguration().Configure(modelBuilder.Entity<PetEntity>());

        new TagEntityTypeConfiguration().Configure(modelBuilder.Entity<TagEntity>());
        new TagReportEntityTypeConfiguration().Configure(modelBuilder.Entity<TagReportEntity>());
        new TagFeatureEntityTypeConfiguration().Configure(modelBuilder.Entity<TagFeatureEntity>());
        new TagHistoryEntryEntityTypeConfiguration().Configure(modelBuilder.Entity<TagHistoryEntryEntity>());

        base.OnModelCreating(modelBuilder);
    }
}
