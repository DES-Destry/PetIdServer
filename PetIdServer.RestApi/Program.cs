using PetIdServer.Application.Extensions;
using PetIdServer.Infrastructure.Extensions;
using PetIdServer.RestApi.Response.Error.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddApplication()
    .AddInfrastructure(configuration, environment)
    .AddServerErrorHandling();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseExceptionHandler("/error");
app.MapControllers();

app.Run();