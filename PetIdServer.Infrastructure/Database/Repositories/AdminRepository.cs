using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class AdminRepository(IMapper mapper, PetIdContext database) : IAdminRepository
{
    public async Task<Admin?> GetAdminById(AdminId id)
    {
        var model = await database.Admins.FirstOrDefaultAsync(admin => admin.Username == id.Value);
        return model is null ? null : mapper.Map<AdminModel, Admin>(model);
    }

    public async Task<Admin?> GetAdminByUsername(string username)
    {
        var model = await database.Admins.FirstOrDefaultAsync(admin => admin.Username == username);
        return model is null ? null : mapper.Map<AdminModel, Admin>(model);
    }

    public async Task<Admin?> CreateAdmin(Admin admin)
    {
        var model = mapper.Map<Admin, AdminModel>(admin);
        var saved = await database.Admins.AddAsync(model);
        await database.SaveChangesAsync();

        return mapper.Map<AdminModel, Admin>(saved.Entity);
    }

    public async Task UpdateAdmin(AdminId id, Admin admin)
    {
        var incomingData = mapper.Map<Admin, AdminModel>(admin);
        var model = await database.Admins.FirstOrDefaultAsync(adminModel => adminModel.Username == id.Value);

        if (model is null) return;

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}