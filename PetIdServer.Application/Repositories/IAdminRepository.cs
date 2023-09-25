using PetIdServer.Core.Entities;
using PetIdServer.Core.Entities.Id;

namespace PetIdServer.Application.Repositories;

public interface IAdminRepository
{
    Task<Admin?> GetAdminById(AdminId id);
    Task<Admin?> GetAdminByUsername(string username);
    Task<Admin?> CreateAdmin(Admin admin);
    Task UpdateAdmin(AdminId adminId, Admin admin);
}