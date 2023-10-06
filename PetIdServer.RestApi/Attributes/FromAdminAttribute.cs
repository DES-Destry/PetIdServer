using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetIdServer.RestApi.Binding;
using PetIdServer.RestApi.Binding.Binders;

namespace PetIdServer.RestApi.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class FromAdminAttribute : Attribute, IModelNameProvider, IBindingSourceMetadata, IBinderTypeProviderMetadata
{
    public string? Name => BindingSource?.Id;
    public BindingSource? BindingSource => PetIdBindingSource.Admin;
    public Type? BinderType => typeof(AdminBinder);
}