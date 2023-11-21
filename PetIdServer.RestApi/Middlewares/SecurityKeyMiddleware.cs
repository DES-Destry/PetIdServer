using System.Security.Cryptography;
using System.Text;
using PetIdServer.Application.Exceptions;
using PetIdServer.RestApi.Attributes;

namespace PetIdServer.RestApi.Middlewares;

public class SecurityKeyMiddleware
{
    private const string SecurityKeyHeader = "x-security-key";
    
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public SecurityKeyMiddleware(
        RequestDelegate next, 
        ILogger<SecurityKeyMiddleware> logger,
        IConfiguration configuration, 
        IWebHostEnvironment environment)
    {
        _next = next;
        _configuration = configuration;
        _environment = environment;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var endpoint = httpContext.GetEndpoint();
        var isSecurityCodeRequired = endpoint?.Metadata.GetMetadata<RequireSecurityKeyAttribute>();

        if (isSecurityCodeRequired is null || _environment.IsDevelopment())
        {
            await _next(httpContext);
            return;
        }

        if (!httpContext.Request.Headers.TryGetValue(SecurityKeyHeader, out var securityKey))
            throw new YourMomIsBitchException();

        var expectedKey = GetExpectedKey();

        if (!securityKey.Equals(expectedKey))
            throw new YourMomIsBitchException();
        
        await _next(httpContext);
    }

    private string GetExpectedKey()
    {
        var date = DateTime.UtcNow.ToString("O")[..15];

        var privatePart = _configuration["Security:SecurityKeySecret"] ??
                          throw new MisconfigurationException().WithMeta(new
                          {
                              _configuration,
                              value = "Security:SecurityKeySecret",
                          });
        var dateSecret = $"{privatePart}_{date}";
        var srcBytes = Encoding.UTF8.GetBytes(dateSecret);
        var hashBytes = MD5.HashData(srcBytes);

        var hexString = BitConverter.ToString(hashBytes);
        return hexString.Replace("-", "");
    }
}