using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PetIdServer.Application.AppDomain.AdminDomain;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class AdminRepository(IMapper mapper, PetIdContext database, NpgsqlDataSource source)
    : IAdminRepository
{
    public async Task<AdminEntity?> GetAdminById(AdminId id)
    {
        var model = await database.Admins.FirstOrDefaultAsync(admin => admin.Username == id);
        return model is null ? null : mapper.Map<AdminModel, AdminEntity>(model);
    }

    public async Task<AdminEntity?> GetAdminByUsername(string username)
    {
        var model = await database.Admins.FirstOrDefaultAsync(admin => admin.Username == username);
        return model is null ? null : mapper.Map<AdminModel, AdminEntity>(model);
    }

    public async Task CreateAdmin(AdminEntity admin)
    {
        var model = mapper.Map<AdminEntity, AdminModel>(admin);
        database.Entry(model).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdateAdmin(AdminId id, AdminEntity admin)
    {
        var incomingData = mapper.Map<AdminEntity, AdminModel>(admin);
        var model =
            await database.Admins.FirstOrDefaultAsync(adminModel =>
                adminModel.Username == id);

        if (model is null) return;

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
