using PetIdServer.AppHost;
using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> pgUsername = builder.AddParameter("pgUsername", true);
IResourceBuilder<ParameterResource> pgPassword = builder.AddParameter("pgPassword", true);

IResourceBuilder<PostgresServerResource> pgServer = builder
    .AddPostgres(Constants.PostgresServer, port: Constants.PostgresPort, userName: pgUsername, password: pgPassword)
    .WithDataVolume(Constants.PostgresVolumeName)
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<PostgresDatabaseResource> pgDatabase =
    pgServer.AddDatabase(Constants.PostgresDatabase, Constants.PostgresDatabaseName);

IResourceBuilder<ProjectResource> migrationRunner = builder
    .AddProject<PetIdServer_Persistence_MigrationRunner>(Constants.MigrationRunnerName)
    .WithReference(pgDatabase)
    .WaitFor(pgDatabase);

builder
    .AddProject<PetIdServer_RestApi>(Constants.AppName)
    .WithReference(pgDatabase)
    .WaitFor(pgDatabase)
    .WaitFor(migrationRunner);

builder.Build().Run();
