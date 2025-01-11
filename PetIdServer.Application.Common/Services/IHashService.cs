using PetIdServer.Core.Users;

namespace PetIdServer.Application.Common.Services;

public interface IHashService
{
    Task<PasswordHash> Hash(string password);
    Task<bool> Validate(string password, string hash);
}
