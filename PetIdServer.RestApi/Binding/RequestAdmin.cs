using System.Security.Claims;
using System.Text.Json;
using PetIdServer.Application.Services.Dto;

namespace PetIdServer.RestApi.Binding;

public class RequestAdmin : AdminDto
{
    public static ValueTask<RequestAdmin> BindAsync(HttpContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        var claimsPrincipal = context.User;
        var admin = ExtractAdmin(claimsPrincipal);

        return ValueTask.FromResult(admin);
    }

    public static RequestAdmin ExtractAdmin(ClaimsPrincipal claimsPrincipal)
    {
        RequestAdmin? result = default;
        foreach (var claim in claimsPrincipal.Claims)
            if (claim.Type == ClaimTypes.UserData)
                result = JsonSerializer.Deserialize<RequestAdmin>(claim.Value) ??
                         throw new ArgumentException(nameof(claim));

        return result ?? throw new ArgumentException(nameof(result));
    }
}
