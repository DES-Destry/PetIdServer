using PetIdServer.Core.Domain.Admin;

namespace PetIdServer.Application.AppDomain.AdminDomain;

public interface IAdminRepository
{
    Task<Admin?> GetAdminById(AdminId id);
    Task<Admin?> GetAdminByUsername(string username);
    Task<Admin?> CreateAdmin(Admin admin);
    Task UpdateAdmin(AdminId adminId, Admin admin);
}
