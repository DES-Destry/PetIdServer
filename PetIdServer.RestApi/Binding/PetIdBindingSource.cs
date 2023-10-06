using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PetIdServer.RestApi.Binding;

public class PetIdBindingSource
{
    public static readonly BindingSource Owner = new (nameof(Owner), "BindingSource_Owner", true, false);
    public static readonly BindingSource Admin = new (nameof(Admin), "BindingSource_Admin", true, false);
}