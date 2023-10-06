using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PetIdServer.Core.Entities;
using PetIdServer.Core.Entities.Id;
using PetIdServer.RestApi.Binding.Binders;

namespace PetIdServer.RestApi.Binding.Types;

[ModelBinder(BinderType = typeof(OwnerBinder))]
public class RequestOwner : Owner
{
    public RequestOwner(CreationAttributes creationAttributes) : base(creationAttributes)
    {
    }

    public RequestOwner(OwnerId id) : base(id)
    {
    }
    
    public static ValueTask<RequestOwner> BindAsync(HttpContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));
        
        var claimsPrincipal = context.User;
        var owner = ExtractOwner(claimsPrincipal);
        
        return ValueTask.FromResult(owner);
    }
    
    public static RequestOwner ExtractOwner(ClaimsPrincipal claimsPrincipal)
    {
        RequestOwner? result = default;
        foreach (var claim in claimsPrincipal.Claims)
        {
            if (claim.Type == ClaimTypes.UserData)
                result = JsonSerializer.Deserialize<RequestOwner>(claim.Value) ?? throw new ArgumentException(nameof(claim));
        }

        return result ?? throw new ArgumentException(nameof(result));;
    }
}