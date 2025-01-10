using System.Security.Claims;
using System.Text.Json;
using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.RestApi.Binding;

public class RequestUser : UserDto
{
    public static ValueTask<RequestUser> BindAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        ClaimsPrincipal claimsPrincipal = context.User;
        RequestUser user = ExtractUser(claimsPrincipal);

        return ValueTask.FromResult(user);
    }

    public static RequestUser ExtractUser(ClaimsPrincipal claimsPrincipal)
    {
        RequestUser? result = null;

        foreach (Claim? claim in claimsPrincipal.Claims)
        {
            if (claim.Type == ClaimTypes.UserData)
            {
                result = JsonSerializer.Deserialize<RequestUser>(claim.Value) ??
                         throw new ArgumentException(nameof(claim));
            }
        }

        return result ?? throw new ArgumentException(nameof(result));
    }
}
