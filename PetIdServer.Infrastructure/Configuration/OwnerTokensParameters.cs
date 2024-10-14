using Microsoft.Extensions.Configuration;
using PetIdServer.Application.Common.Exceptions;

namespace PetIdServer.Infrastructure.Configuration;

public class OwnerTokensParameters(IConfiguration configuration)
{
    public string AtSecret { get; private set; } = configuration["Jwt:Owner:Access:Secret"] ??
                                                   throw new MisconfigurationException().WithMeta(new
                                                   {
                                                       configuration,
                                                       value = "Jwt:Owner:Access:Secret",
                                                       @class = nameof(OwnerTokensParameters)
                                                   });

    public string RtSecret { get; private set; } = configuration["Jwt:Owner:Refresh:Secret"] ??
                                                   throw new MisconfigurationException().WithMeta(new
                                                   {
                                                       configuration,
                                                       value = "Jwt:Owner:Refresh:Secret",
                                                       @class = nameof(OwnerTokensParameters)
                                                   });

    public string AtTtl { get; private set; } = configuration["Jwt:Owner:Access:Ttl"] ??
                                                throw new MisconfigurationException().WithMeta(new
                                                {
                                                    configuration,
                                                    value = "Jwt:Owner:Access:Ttl",
                                                    @class = nameof(OwnerTokensParameters)
                                                });

    public string RtTtl { get; private set; } = configuration["Jwt:Owner:Refresh:Ttl"] ??
                                                throw new MisconfigurationException().WithMeta(new
                                                {
                                                    configuration,
                                                    value = "Jwt:Owner:Refresh:Ttl",
                                                    @class = nameof(OwnerTokensParameters)
                                                });

    public string Issuer { get; private set; } = configuration["Jwt:Issuer"] ??
                                                 throw new MisconfigurationException().WithMeta(new
                                                 {
                                                     configuration, value = "Jwt:Issuer", @class = nameof(OwnerTokensParameters)
                                                 });

    public string Audience { get; private set; } = configuration["Jwt:Audience"] ??
                                                   throw new MisconfigurationException().WithMeta(new
                                                   {
                                                       configuration,
                                                       value = "Jwt:Audience",
                                                       @class = nameof(OwnerTokensParameters)
                                                   });
}
