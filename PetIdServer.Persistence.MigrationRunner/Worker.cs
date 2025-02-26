using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;
using PetIdServer.Persistence.Mapper;

namespace PetIdServer.Persistence.MigrationRunner;

public class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
    : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Start Migration Runner Worker :)");
        using Activity? activity = s_activitySource.StartActivity(ActivityKind.Client);

        try
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            PetIdContext dbContext = scope.ServiceProvider.GetRequiredService<PetIdContext>();

            logger.LogInformation("Ensure, that database exists...");
            await EnsureDatabaseAsync(dbContext, cancellationToken);
            logger.LogInformation("Database exists or has been created!");

            logger.LogInformation("Run EF migration...");
            await RunMigrationAsync(dbContext, cancellationToken);
            logger.LogInformation("EF Migration completed!");

            logger.LogInformation("Seed data... Add default admin for system");
            await SeedDataAsync(dbContext, cancellationToken);
            logger.LogInformation("Data has been added int database!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occured while executing migration and seeding");
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task EnsureDatabaseAsync(PetIdContext dbContext, CancellationToken cancellationToken)
    {
        IRelationalDatabaseCreator dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(PetIdContext dbContext, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        }).ConfigureAwait(false);
    }

    private static async Task SeedDataAsync(PetIdContext dbContext, CancellationToken cancellationToken)
    {
        User initialAdmin =
            User.CreateNew(new User.CreationAttributes(
                               "dev.andrey.kirik@gmail.com",
                               "Andrey Kirik",
                               null,
                               [UserRole.MostPrivileged]));

        initialAdmin.Update(new User.UpdateAttributes
        {
            Address = "Los Angeles", Description = "The first and the most privileged user in the system"
        });

        UserEntity adminEntity = initialAdmin.ToEntity();

        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(
            token => SeedAdminIfNotExistsAsync(dbContext, adminEntity, token),
            cancellationToken);
    }

    private static async Task SeedAdminIfNotExistsAsync(PetIdContext dbContext,
        UserEntity adminEntity,
        CancellationToken cancellationToken)
    {
        bool isAdminCreated = await dbContext.Users.AnyAsync(u => u.Email == adminEntity.Email, cancellationToken);

        if (isAdminCreated)
        {
            return;
        }

        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        await dbContext.Users.AddAsync(adminEntity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}
