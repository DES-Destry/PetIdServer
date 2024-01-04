using PetIdServer.Core.Domain.Admin;

namespace PetIdServer.Application.Domain.Admin;

public interface IAdminRepository
{
    Task<Core.Domain.Admin.Admin?> GetAdminById(AdminId id);
    Task<Core.Domain.Admin.Admin?> GetAdminByUsername(string username);
    Task<Core.Domain.Admin.Admin?> CreateAdmin(Core.Domain.Admin.Admin admin);
    Task UpdateAdmin(AdminId adminId, Core.Domain.Admin.Admin admin);
}
