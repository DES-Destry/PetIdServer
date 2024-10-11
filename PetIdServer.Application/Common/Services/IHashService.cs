namespace PetIdServer.Application.Common.Services;

public interface IHashService
{
    Task<string> Hash(string password);
    Task<bool> Validate(string password, string hash);
}
