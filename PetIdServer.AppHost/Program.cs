using PetIdServer.AppHost;
using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> pgUsername = builder.AddParameter("pgUsername", true);
IResourceBuilder<ParameterResource> pgPassword = builder.AddParameter("pgPassword", true);

IResourceBuilder<PostgresServerResource> pgServer = builder
    .AddPostgres(AspireConstants.PostgresServer, port: AspireConstants.PostgresPort, userName: pgUsername, password: pgPassword)
    .WithDataVolume(AspireConstants.PostgresVolumeName)
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<PostgresDatabaseResource> pgDatabase =
    pgServer.AddDatabase(AspireConstants.PostgresDatabase, AspireConstants.PostgresDatabaseName);

IResourceBuilder<ProjectResource> migrationRunner = builder
    .AddProject<PetIdServer_Persistence_MigrationRunner>(AspireConstants.MigrationRunnerName)
    .WithReference(pgDatabase)
    .WaitFor(pgDatabase);

builder
    .AddProject<PetIdServer_RestApi>(AspireConstants.AppName)
    .WithReference(pgDatabase)
    .WaitFor(pgDatabase)
    .WaitFor(migrationRunner);

builder.Build().Run();
