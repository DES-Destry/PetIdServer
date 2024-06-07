using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var pgPassword = builder.AddParameter("pgPassword", true);

var db = builder
    .AddPostgres("pg", port: 5432, password: pgPassword)
    .WithDataVolume("petidserver_pet-id_pgdata")
    .AddDatabase("pet-id");

builder
    .AddProject<PetIdServer_RestApi>("api")
    .WithReference(db);

builder.Build().Run();
