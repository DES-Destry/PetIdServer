using Carter;
using PetIdServer.Infrastructure.Extensions;
using PetIdServer.RestApi.Extensions;
using PetIdServer.RestApi.Response.Error.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddCors(options =>
                             options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services
    .AddEndpointsApiExplorer()
    .AddServerErrorHandling()
    .AddSwagger()
    .AddInfrastructure(builder, configuration)
    .AddCarter()
    .AddPetIdAuthPolicies();

builder.Services.AddAuthentication().AddPetIdAuthSchemas(configuration);

WebApplication app = builder.Build();

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
