using Carter;
using PetIdServer.Application.Common.Extensions;
using PetIdServer.Infrastructure.Extensions;
using PetIdServer.RestApi.Extensions;
using PetIdServer.RestApi.Mapper;
using PetIdServer.RestApi.Response.Error.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var configuration = builder.Configuration;

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddAuthentication().AddPetIdAuthSchemas(configuration);
builder.Services
    .AddEndpointsApiExplorer()
    .AddServerErrorHandling()
    .AddSwagger()
    .AddApplication()
    .AddInfrastructure(builder)
    .AddAutoMapper(typeof(RestApiMappingProfile))
    .AddCarter()
    .AddPetIdAuthPolicies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.MapDefaultEndpoints();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSecurityKey();
app.MapCarter();

app.Run();
