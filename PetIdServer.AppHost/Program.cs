using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db");
var redis = builder.AddRedis("redis");

builder.AddProject<PetIdServer_RestApi>("api")
    .WithReference(db)
    .WithReference(redis);

builder.Build().Run();
