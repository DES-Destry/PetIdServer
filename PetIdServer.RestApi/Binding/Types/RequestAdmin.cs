using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PetIdServer.Core.Entities;
using PetIdServer.Core.Entities.Id;
using PetIdServer.RestApi.Binding.Binders;

namespace PetIdServer.RestApi.Binding.Types;

[ModelBinder(BinderType = typeof(AdminBinder))]
public class RequestAdmin : Admin
{
    public RequestAdmin(CreationAttributes creationAttributes) : base(creationAttributes)
    {
    }

    public RequestAdmin(AdminId id) : base(id)
    {
    }

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
        {
            if (claim.Type == ClaimTypes.UserData)
                result = JsonSerializer.Deserialize<RequestAdmin>(claim.Value) ?? throw new ArgumentException(nameof(claim));
        }

        return result ?? throw new ArgumentException(nameof(result));
    }
}