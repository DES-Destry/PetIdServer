using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Primitives;
using PetIdServer.Application.Common.Exceptions;
using PetIdServer.Infrastructure.Exceptions;
using PetIdServer.RestApi.Attributes;

namespace PetIdServer.RestApi.Middlewares;

public class SecurityKeyMiddleware(
    RequestDelegate next,
    ILogger<SecurityKeyMiddleware> logger,
    IConfiguration configuration,
    IWebHostEnvironment environment)
{
    private const string SecurityKeyHeader = "x-security-key";

    private readonly ILogger<SecurityKeyMiddleware> _logger = logger;

    public async Task Invoke(HttpContext httpContext)
    {
        Endpoint? endpoint = httpContext.GetEndpoint();
        RequireSecurityKeyAttribute? isSecurityCodeRequired = endpoint?.Metadata.GetMetadata<RequireSecurityKeyAttribute>();

        if (isSecurityCodeRequired is null || environment.IsDevelopment())
        {
            await next(httpContext);
            return;
        }

        if (!httpContext.Request.Headers.TryGetValue(SecurityKeyHeader, out StringValues securityKey))
        {
            throw new YourMomIsBitchException();
        }

        string? expectedKey = GetExpectedKey();

        if (!securityKey.Equals(expectedKey))
        {
            throw new YourMomIsBitchException();
        }

        await next(httpContext);
    }

    private string GetExpectedKey()
    {
        string? date = DateTime.UtcNow.ToString("O")[..15];

        string? privatePart = configuration["Security:SecurityKeySecret"] ??
                              throw new MisconfigurationException().WithMeta(new
                              {
                                  _configuration = configuration, value = "Security:SecurityKeySecret"
                              });
        string? dateSecret = string.Concat(privatePart, "_", date);
        byte[]? srcBytes = Encoding.UTF8.GetBytes(dateSecret);
        byte[]? hashBytes = MD5.HashData(srcBytes);

        string? hexString = BitConverter.ToString(hashBytes);
        return hexString.Replace("-", "");
    }
}
