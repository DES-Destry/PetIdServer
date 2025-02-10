using PetIdServer.Application.Common.Services;

namespace PetIdServer.Infrastructure.Services.Secrets;

public class AwsSecretManager : ISecretManager<ConfigKeyForSecret>
{
    public async Task<string> GetSecretAsync(ConfigKeyForSecret name) => throw new NotImplementedException();
    public async Task<string?> GetSecretOrDefaultAsync(ConfigKeyForSecret name) => throw new NotImplementedException();
}
