using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetIdServer.RestApi.Binding.Types;

namespace PetIdServer.RestApi.Binding.Binders;

public class OwnerBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));
        
        var claimsPrincipal = bindingContext.HttpContext.User;
        var owner = RequestOwner.ExtractOwner(claimsPrincipal);
        
        bindingContext.Result = ModelBindingResult.Success(owner);
        return Task.CompletedTask;
    }
}