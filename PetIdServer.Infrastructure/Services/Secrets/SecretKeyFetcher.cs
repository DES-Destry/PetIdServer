using Microsoft.Extensions.Configuration;
using PetIdServer.Infrastructure.Exceptions;

namespace PetIdServer.Infrastructure.Services.Secrets;

public sealed class SecretKeyFetcher(IConfiguration configuration)
{
    public string GetKeyValue(ConfigKeyForSecret configKeyForSecret) => configuration[configKeyForSecret] ??
                                                                        throw new MisconfigurationException().WithMeta(new
                                                                        {
                                                                            @class = nameof(SecretKeyFetcher),
                                                                            configuration,
                                                                            value = configKeyForSecret
                                                                        });
}
