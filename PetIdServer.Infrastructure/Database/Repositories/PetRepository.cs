using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class PetRepository : IPetRepository
{
    private readonly IMapper _mapper;
    private readonly PetIdContext _database;

    public PetRepository(IMapper mapper, PetIdContext database)
    {
        _mapper = mapper;
        _database = database;
    }

    public async Task<Pet?> GetPetById(Guid id)
    {
        var model = await _database.Pets.AsNoTracking().FirstOrDefaultAsync(petModel => petModel.Id == id);
        return model is null ? null : _mapper.Map<PetModel, Pet>(model);
    }

    public async Task<Pet?> CreatePet(Pet pet)
    {
        var model = _mapper.Map<Pet, PetModel>(pet);
        var saved = await _database.Pets.AddAsync(model);

        return _mapper.Map<PetModel, Pet>(saved.Entity);
    }

    public async Task UpdatePet(Guid petId, Pet pet)
    {
        var incomingData = _mapper.Map<Pet, PetModel>(pet);
        var model = await _database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == petId);
        
        if (model is null) return;
        
        _database.Entry(model).CurrentValues.SetValues(incomingData);
        await _database.SaveChangesAsync();
    }
}