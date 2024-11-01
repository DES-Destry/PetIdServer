using PetIdServer.Core.Domain.Admin;

namespace PetIdServer.Application.Domain.Admin;

public interface IAdminRepository
{
    Task<AdminEntity?> GetAdminById(AdminId id);
    Task<AdminEntity?> GetAdminByUsername(string username);
    Task CreateAdmin(AdminEntity admin);
    Task UpdateAdmin(AdminId adminId, AdminEntity admin);
}
