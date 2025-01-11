using Microsoft.Extensions.Configuration;
using PetIdServer.Infrastructure.Exceptions;

namespace PetIdServer.Infrastructure.Configuration;

public class JwtTokensParameters(IConfiguration configuration)
{
    public string AtSecret { get; private set; } = configuration["Jwt:Access:Secret"] ??
                                                   throw new MisconfigurationException().WithMeta(new
                                                   {
                                                       configuration,
                                                       value = "Jwt:Access:Secret",
                                                       @class = nameof(JwtTokensParameters)
                                                   });

    public string RtSecret { get; private set; } = configuration["Jwt:Refresh:Secret"] ??
                                                   throw new MisconfigurationException().WithMeta(new
                                                   {
                                                       configuration,
                                                       value = "Jwt:Refresh:Secret",
                                                       @class = nameof(JwtTokensParameters)
                                                   });

    public string PetOwnerAtTtl { get; private set; } = configuration["Jwt:Access:Ttl:PetOwner"] ??
                                                        throw new MisconfigurationException().WithMeta(new
                                                        {
                                                            configuration,
                                                            value = "Jwt:Access:Ttl:PetOwner",
                                                            @class = nameof(JwtTokensParameters)
                                                        });

    public string PetOwnerRtTtl { get; private set; } = configuration["Jwt:Refresh:Ttl:PetOwner"] ??
                                                        throw new MisconfigurationException().WithMeta(new
                                                        {
                                                            configuration,
                                                            value = "Jwt:Refresh:Ttl:PetOwner",
                                                            @class = nameof(JwtTokensParameters)
                                                        });

    public string OtherAtTtl { get; private set; } = configuration["Jwt:Access:Ttl:Other"] ??
                                                     throw new MisconfigurationException().WithMeta(new
                                                     {
                                                         configuration,
                                                         value = "Jwt:Access:Ttl:Other",
                                                         @class = nameof(JwtTokensParameters)
                                                     });

    public string Issuer { get; private set; } = configuration["Jwt:Issuer"] ??
                                                 throw new MisconfigurationException().WithMeta(new
                                                 {
                                                     configuration, value = "Jwt:Issuer", @class = nameof(JwtTokensParameters)
                                                 });

    public string Audience { get; private set; } = configuration["Jwt:Audience"] ??
                                                   throw new MisconfigurationException().WithMeta(new
                                                   {
                                                       configuration, value = "Jwt:Audience", @class = nameof(JwtTokensParameters)
                                                   });
}
