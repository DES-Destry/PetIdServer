using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Domain.Pet;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class PetRepository(IMapper mapper, PetIdContext database) : IPetRepository
{
    public async Task<Pet?> GetPetById(PetId id)
    {
        var model = await database.Pets.AsNoTracking()
            .FirstOrDefaultAsync(petModel => petModel.Id == id.Value);
        return model is null ? null : mapper.Map<PetModel, Pet>(model);
    }

    public async Task<Pet?> CreatePet(Pet pet)
    {
        var model = mapper.Map<Pet, PetModel>(pet);
        var saved = await database.Pets.AddAsync(model);

        return mapper.Map<PetModel, Pet>(saved.Entity);
    }

    public async Task UpdatePet(PetId id, Pet pet)
    {
        var incomingData = mapper.Map<Pet, PetModel>(pet);
        var model = await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == id.Value);

        if (model is null) return;

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
