using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Repositories;

public interface IAdminRepository
{
    Task<Admin?> GetAdminByUsername(string username);
    Task<Admin?> CreateAdmin(Admin admin);
    Task UpdateAdmin(string username, Admin admin);
}