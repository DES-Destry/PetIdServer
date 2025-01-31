namespace PetIdServer.Application.Common.Services;

public interface ISecretManager
{
    Task<string> GetSecretAsync(string name);
    Task<string?> GetSecretOrDefaultAsync(string name);
}
