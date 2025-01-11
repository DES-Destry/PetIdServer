using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Pets;
using PetIdServer.Core.Pets;
using PetIdServer.Infrastructure.Database.Entities;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class PetRepository(IMapper mapper, PetIdContext database) : IPetRepository
{
    public async Task CreatePet(Pet pet)
    {
        PetEntity? model = mapper.Map<Pet, PetEntity>(pet);
        database.Entry(model).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdatePet(PetId id, Pet pet)
    {
        PetEntity? incomingData = mapper.Map<Pet, PetEntity>(pet);
        PetEntity? model = await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }

    public async Task<Pet?> GetPetById(PetId id)
    {
        PetEntity? model = await database.Pets.AsNoTracking()
            .FirstOrDefaultAsync(petModel => petModel.Id == id);
        return model is null ? null : mapper.Map<PetEntity, Pet>(model);
    }
}
