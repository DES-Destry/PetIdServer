using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder
    .AddPostgres("db")
    .WithDataVolume("petidserver_pet-id_pgdata")
    .AddDatabase("pet-id");

var redis = builder
    .AddRedis("redis")
    .WithDataVolume("petidserver_pet-id_redis-data");

builder.AddProject<PetIdServer_RestApi>("api")
    .WithReference(db)
    .WithReference(redis);

builder.Build().Run();
