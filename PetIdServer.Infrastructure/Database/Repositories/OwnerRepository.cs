using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly IMapper _mapper;
    private readonly PetIdContext _database;

    public OwnerRepository(IMapper mapper, PetIdContext database)
    {
        _mapper = mapper;
        _database = database;
    }
    
    public async Task<Owner?> GetOwnerById(OwnerId id)
    {
        var model = await _database.Owners.AsNoTracking().FirstOrDefaultAsync(owner => owner.Email == id.Value);
        return model is null ? null : _mapper.Map<OwnerModel, Owner>(model);
    }

    public async Task<Owner?> GetOwnerByEmail(string email)
    {
        var model = await _database.Owners.AsNoTracking().FirstOrDefaultAsync(owner => owner.Email == email);
        return model is null ? null : _mapper.Map<OwnerModel, Owner>(model);
    }

    public async Task<Owner?> CreateOwner(Owner owner)
    {
        var model = _mapper.Map<Owner, OwnerModel>(owner);
        var saved = await _database.Owners.AddAsync(model);
        await _database.SaveChangesAsync();
        
        return _mapper.Map<OwnerModel, Owner>(saved.Entity);
    }

    public async Task UpdateOwner(OwnerId id, Owner owner)
    {
        var incomingData = _mapper.Map<Owner, OwnerModel>(owner);
        var model = await _database.Owners.FirstOrDefaultAsync(ownerModel => ownerModel.Email == id.Value);
        
        if (model is null) return;

        _database.Entry(model).CurrentValues.SetValues(incomingData);
        await _database.SaveChangesAsync();
    }
}