using Microsoft.EntityFrameworkCore;

namespace PetIdServer.Infrastructure.Database;

public class PetIdContext : DbContext
{
    public PetIdContext(DbContextOptions<PetIdContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("pet");
        
        // Default Guid Id value
        
        base.OnModelCreating(modelBuilder);
    }
}