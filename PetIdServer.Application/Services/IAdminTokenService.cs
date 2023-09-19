using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Services;

public interface IAdminTokenService
{
    Task<string> GenerateToken(Admin admin);
    Task<Admin> DecryptAdmin(string token);
}