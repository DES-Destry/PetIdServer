using PetIdServer.Persistence;
using PetIdServer.Persistence.MigrationRunner;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<PetIdContext>("PetIdPostgresDb");

IHost host = builder.Build();
host.Run();
