using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var pgPassword = builder.AddParameter("pgPassword", true);

var db = builder
    .AddPostgres("pg", password: pgPassword)
    .WithDataVolume("petidserver_pet-id_pgdata")
    .AddDatabase("pet-id");


var redis = builder
    .AddRedis("redis")
    .WithDataVolume("petidserver_pet-id_redis-data");

builder.AddProject<PetIdServer_RestApi>("api")
    .WithReference(db)
    .WithReference(redis);

builder.Build().Run();
