using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<PetIdServer_RestApi>("api");

builder.Build().Run();
