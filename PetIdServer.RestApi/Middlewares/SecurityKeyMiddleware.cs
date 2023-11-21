using System.Security.Cryptography;
using System.Text;
using PetIdServer.Application.Exceptions;
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
        var endpoint = httpContext.GetEndpoint();
        var isSecurityCodeRequired = endpoint?.Metadata.GetMetadata<RequireSecurityKeyAttribute>();

        if (isSecurityCodeRequired is null || environment.IsDevelopment())
        {
            await next(httpContext);
            return;
        }

        if (!httpContext.Request.Headers.TryGetValue(SecurityKeyHeader, out var securityKey))
            throw new YourMomIsBitchException();

        var expectedKey = GetExpectedKey();

        if (!securityKey.Equals(expectedKey))
            throw new YourMomIsBitchException();

        await next(httpContext);
    }

    private string GetExpectedKey()
    {
        var date = DateTime.UtcNow.ToString("O")[..15];

        var privatePart = configuration["Security:SecurityKeySecret"] ??
                          throw new MisconfigurationException().WithMeta(new
                          {
                              _configuration = configuration,
                              value = "Security:SecurityKeySecret",
                          });
        var dateSecret = string.Concat(privatePart, "_", date);
        var srcBytes = Encoding.UTF8.GetBytes(dateSecret);
        var hashBytes = MD5.HashData(srcBytes);

        var hexString = BitConverter.ToString(hashBytes);
        return hexString.Replace("-", "");
    }
}