using System.Text;
using Carter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PetIdServer.Application.Extensions;
using PetIdServer.Infrastructure.Configuration;
using PetIdServer.Infrastructure.Extensions;
using PetIdServer.RestApi.Auth;
using PetIdServer.RestApi.Extensions;
using PetIdServer.RestApi.Mapper;
using PetIdServer.RestApi.Response.Error.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

var ownerTokenParameters = new OwnerTokenParameters(configuration);
var adminTokenParameters = new AdminTokenParameters(configuration);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddApplication()
    .AddInfrastructure(configuration, environment)
    .AddServerErrorHandling()
    .AddAutoMapper(typeof(RestApiMappingProfile))
    .AddCarter();
builder.Services.AddAuthentication()
    .AddJwtBearer(AuthSchemas.Owner, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = ownerTokenParameters.Issuer,
            ValidAudience = ownerTokenParameters.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ownerTokenParameters.AtSecret))
        };
    })
    .AddJwtBearer(AuthSchemas.Admin, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = adminTokenParameters.Issuer,
            ValidAudience = adminTokenParameters.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(adminTokenParameters.JwtSecret))
        };
    });
builder.Services.AddAuthorization(options =>
{
    var ownerPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(AuthSchemas.Owner)
        .Build();

    var adminPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(AuthSchemas.Admin)
        .Build();

    options.AddPolicy(AuthSchemas.Owner, ownerPolicy);
    options.AddPolicy(AuthSchemas.Admin, adminPolicy);
});

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
app.MapCarter();

app.Run();