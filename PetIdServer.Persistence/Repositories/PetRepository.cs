using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Pets;
using PetIdServer.Core.Pets;
using PetIdServer.Persistence.Entities;
using PetIdServer.Persistence.Mapper;

namespace PetIdServer.Persistence.Repositories;

public class PetRepository(PetIdContext database) : IPetRepository
{
    public async Task<Pet?> GetPetById(PetId id)
    {
        PetEntity? entity = await database.Pets.AsNoTracking()
            .FirstOrDefaultAsync(petModel => petModel.Id == id);
        return entity?.ToCore();
    }

    public async Task CreatePet(Pet pet)
    {
        PetEntity entity = pet.ToEntity();
        database.Entry(entity).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdatePet(PetId id, Pet pet)
    {
        PetEntity incomingData = pet.ToEntity();
        PetEntity? entity = await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == id);

        if (entity is null)
        {
            return;
        }

        database.Entry(entity).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
