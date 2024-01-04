using Microsoft.Extensions.Configuration;
using PetIdServer.Application.Common.Exceptions;

namespace PetIdServer.Infrastructure.Configuration;

public class OwnerTokenParameters
{
    public OwnerTokenParameters(IConfiguration configuration)
    {
        AtSecret = configuration["Jwt:Owner:Access:Secret"] ??
                   throw new MisconfigurationException().WithMeta(new
                   {
                       configuration,
                       value = "Jwt:Owner:Access:Secret",
                       @class = nameof(OwnerTokenParameters)
                   });
        RtSecret = configuration["Jwt:Owner:Refresh:Secret"] ??
                   throw new MisconfigurationException().WithMeta(new
                   {
                       configuration,
                       value = "Jwt:Owner:Refresh:Secret",
                       @class = nameof(OwnerTokenParameters)
                   });
        AtTtl = configuration["Jwt:Owner:Access:Ttl"] ??
                throw new MisconfigurationException().WithMeta(new
                {
                    configuration,
                    value = "Jwt:Owner:Access:Ttl",
                    @class = nameof(OwnerTokenParameters)
                });
        RtTtl = configuration["Jwt:Owner:Refresh:Ttl"] ??
                throw new MisconfigurationException().WithMeta(new
                {
                    configuration,
                    value = "Jwt:Owner:Refresh:Ttl",
                    @class = nameof(OwnerTokenParameters)
                });
        Issuer = configuration["Jwt:Issuer"] ??
                 throw new MisconfigurationException().WithMeta(new
                 {
                     configuration,
                     value = "Jwt:Issuer",
                     @class = nameof(OwnerTokenParameters)
                 });
        Audience = configuration["Jwt:Audience"] ??
                   throw new MisconfigurationException().WithMeta(new
                   {
                       configuration,
                       value = "Jwt:Audience",
                       @class = nameof(OwnerTokenParameters)
                   });
    }

    public string AtSecret { get; private set; }
    public string RtSecret { get; private set; }
    public string AtTtl { get; private set; }
    public string RtTtl { get; private set; }
    public string Issuer { get; private set; }
    public string Audience { get; private set; }
}
