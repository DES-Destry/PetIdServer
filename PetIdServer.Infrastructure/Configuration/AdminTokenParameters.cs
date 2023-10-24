using Microsoft.Extensions.Configuration;
using PetIdServer.Application.Exceptions;

namespace PetIdServer.Infrastructure.Configuration;

public class AdminTokenParameters
{
    public string JwtSecret { get; private set; }
    public string JwtTtl { get; private set; }
    public string Issuer { get; private set; }
    public string Audience { get; private set; }

    public AdminTokenParameters(IConfiguration configuration)
    {
        JwtSecret = configuration["Jwt:Admin:Secret"] ??
                     throw new MisconfigurationException().WithMeta(new
                     {
                         configuration,
                         value = "Jwt:Admin:Secret",
                         @class = nameof(AdminTokenParameters),
                     });
        JwtTtl = configuration["Jwt:Admin:Ttl"] ??
                  throw new MisconfigurationException().WithMeta(new
                  {
                      configuration,
                      value = "Jwt:Admin:Ttl",
                      @class = nameof(AdminTokenParameters),
                  });
        Issuer = configuration["Jwt:Issuer"] ??
                  throw new MisconfigurationException().WithMeta(new
                  {
                      configuration,
                      value = "Jwt:Issuer",
                      @class = nameof(AdminTokenParameters),
                  });
        Audience = configuration["Jwt:Audience"] ??
                    throw new MisconfigurationException().WithMeta(new
                    {
                        configuration,
                        value = "Jwt:Audience",
                        @class = nameof(AdminTokenParameters),
                    });
    }
}