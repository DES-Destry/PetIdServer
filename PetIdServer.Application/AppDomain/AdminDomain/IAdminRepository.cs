using PetIdServer.Core.Domain.Admin;

namespace PetIdServer.Application.AppDomain.AdminDomain;

public interface IAdminRepository
{
    Task<AdminEntity?> GetAdminById(AdminId id);
    Task<AdminEntity?> GetAdminByUsername(string username);
    Task<AdminEntity?> CreateAdmin(AdminEntity admin);
    Task UpdateAdmin(AdminId adminId, AdminEntity admin);
}
