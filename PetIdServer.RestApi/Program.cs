using Carter;
using PetIdServer.Application.Extensions;
using PetIdServer.Infrastructure.Configuration;
using PetIdServer.Infrastructure.Extensions;
using PetIdServer.RestApi.Extensions;
using PetIdServer.RestApi.Mapper;
using PetIdServer.RestApi.Response.Error.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var ownerTokenParameters = new OwnerTokenParameters(configuration);
var adminTokenParameters = new AdminTokenParameters(configuration);

builder.Services.AddAuthentication().AddPetIdAuthSchemas(ownerTokenParameters, adminTokenParameters);
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddApplication()
    .AddInfrastructure(configuration, environment)
    .AddServerErrorHandling()
    .AddAutoMapper(typeof(RestApiMappingProfile))
    .AddCarter()
    .AddPetIdAuthPolicies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseExceptionHandler("/error");
app.MapCarter();

app.Run();