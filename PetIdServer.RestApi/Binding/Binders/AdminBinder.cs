using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetIdServer.RestApi.Binding.Types;

namespace PetIdServer.RestApi.Binding.Binders;

public class AdminBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));
        
        var claimsPrincipal = bindingContext.HttpContext.User;
        var admin = RequestAdmin.ExtractAdmin(claimsPrincipal);
        
        bindingContext.Result = ModelBindingResult.Success(admin);
        return Task.CompletedTask;
    }
}