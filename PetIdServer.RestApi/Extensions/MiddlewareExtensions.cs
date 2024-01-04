using PetIdServer.RestApi.Middlewares;

namespace PetIdServer.RestApi.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityKey(
        this IApplicationBuilder builder) => builder.UseMiddleware<SecurityKeyMiddleware>();
}
