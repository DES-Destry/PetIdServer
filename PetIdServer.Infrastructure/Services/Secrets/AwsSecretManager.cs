using PetIdServer.Application.Common.Services;

namespace PetIdServer.Infrastructure.Services.Secrets;

public class AwsSecretManager : ISecretManager<SecretKey>
{
    public async Task<string> GetSecretAsync(SecretKey name) => throw new NotImplementedException();
    public async Task<string?> GetSecretOrDefaultAsync(SecretKey name) => throw new NotImplementedException();
}
