using PetIdServer.Infrastructure.Extensions;
using PetIdServer.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure(builder).AddHostedService<MigrationWorker>();

var host = builder.Build();
host.Run();
