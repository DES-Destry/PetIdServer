using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetIdServer.RestApi.Binding;
using PetIdServer.RestApi.Binding.Binders;

namespace PetIdServer.RestApi.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class FromOwnerAttribute : Attribute, IModelNameProvider, IBinderTypeProviderMetadata
{
    public string? Name => BindingSource?.Id;
    public BindingSource? BindingSource => PetIdBindingSource.Owner;
    public Type? BinderType => typeof(OwnerBinder);
}