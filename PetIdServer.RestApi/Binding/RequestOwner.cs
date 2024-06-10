using System.Security.Claims;
using System.Text.Json;
using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.RestApi.Binding;

public class RequestOwner : OwnerDto
{
    public static ValueTask<RequestOwner> BindAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var claimsPrincipal = context.User;
        var owner = ExtractOwner(claimsPrincipal);

        return ValueTask.FromResult(owner);
    }

    public static RequestOwner ExtractOwner(ClaimsPrincipal claimsPrincipal)
    {
        RequestOwner? result = default;
        foreach (var claim in claimsPrincipal.Claims)
            if (claim.Type == ClaimTypes.UserData)
                result = JsonSerializer.Deserialize<RequestOwner>(claim.Value) ??
                         throw new ArgumentException(nameof(claim));

        return result ?? throw new ArgumentException(nameof(result));
        ;
    }
}
