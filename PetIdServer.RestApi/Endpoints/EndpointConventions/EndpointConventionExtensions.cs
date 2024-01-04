using PetIdServer.RestApi.Attributes;

namespace PetIdServer.RestApi.Endpoints.EndpointConventions;

public static class EndpointConventionExtensions
{
    public static TBuilder RequireSecurityKey<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder =>
            endpointBuilder.Metadata.Add(new RequireSecurityKeyAttribute()));
        return builder;
    }
}
