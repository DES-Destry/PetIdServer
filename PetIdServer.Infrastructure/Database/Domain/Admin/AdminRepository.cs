using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.AppDomain.AdminDomain;
using PetIdServer.Core.Domain.Admin;

namespace PetIdServer.Infrastructure.Database.Domain.Admin;

public class AdminRepository(IMapper mapper, PetIdContext database)
    : IAdminRepository
{
    public async Task<AdminEntity?> GetAdminById(AdminId id)
    {
        AdminModel? model = await database.Admins.FirstOrDefaultAsync(admin => admin.Username == id);
        return model is null ? null : mapper.Map<AdminModel, AdminEntity>(model);
    }

    public async Task<AdminEntity?> GetAdminByUsername(string username)
    {
        AdminModel? model = await database.Admins.FirstOrDefaultAsync(admin => admin.Username == username);
        return model is null ? null : mapper.Map<AdminModel, AdminEntity>(model);
    }

    public async Task CreateAdmin(AdminEntity admin)
    {
        AdminModel? model = mapper.Map<AdminEntity, AdminModel>(admin);
        database.Entry(model).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdateAdmin(AdminId id, AdminEntity admin)
    {
        AdminModel? incomingData = mapper.Map<AdminEntity, AdminModel>(admin);
        AdminModel? model =
            await database.Admins.FirstOrDefaultAsync(adminModel =>
                                                          adminModel.Username == id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
