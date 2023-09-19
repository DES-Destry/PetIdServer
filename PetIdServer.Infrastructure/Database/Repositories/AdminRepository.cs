using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IMapper _mapper;
    private readonly PetIdContext _database;

    public AdminRepository(IMapper mapper, PetIdContext database)
    {
        _mapper = mapper;
        _database = database;
    }

    public async Task<Admin?> GetAdminByUsername(string username)
    {
        var model = await _database.Admins.FirstOrDefaultAsync(admin => admin.Id == username);
        return model is null ? null : _mapper.Map<AdminModel, Admin>(model);
    }

    public async Task<Admin?> CreateAdmin(Admin admin)
    {
        var model = _mapper.Map<Admin, AdminModel>(admin);
        var saved = await _database.Admins.AddAsync(model);
        await _database.SaveChangesAsync();

        return _mapper.Map<AdminModel, Admin>(saved.Entity);
    }

    public async Task UpdateAdmin(string username, Admin admin)
    {
        var incomingData = _mapper.Map<Admin, AdminModel>(admin);
        var model = await _database.Admins.FirstOrDefaultAsync(adminModel => adminModel.Id == username);
        
        if (model is null) return;

        _database.Entry(model).CurrentValues.SetValues(incomingData);
        await _database.SaveChangesAsync();
    }
}