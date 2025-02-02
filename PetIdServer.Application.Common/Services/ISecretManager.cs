namespace PetIdServer.Application.Common.Services;

public interface ISecretManager<in TSecret>
{
    Task<string> GetSecretAsync(TSecret name);
    Task<string?> GetSecretOrDefaultAsync(TSecret name);
}
