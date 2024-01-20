using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.AppDomain.PetDomain;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class PetRepository(IMapper mapper, PetIdContext database) : IPetRepository
{
    public async Task<PetEntity?> GetPetById(PetId id)
    {
        var model = await database.Pets.AsNoTracking()
            .FirstOrDefaultAsync(petModel => petModel.Id == id);
        return model is null ? null : mapper.Map<PetModel, PetEntity>(model);
    }

    public async Task CreatePet(PetEntity pet)
    {
        var model = mapper.Map<PetEntity, PetModel>(pet);
        database.Entry(model).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdatePet(PetId id, PetEntity pet)
    {
        var incomingData = mapper.Map<PetEntity, PetModel>(pet);
        var model = await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == id);

        if (model is null) return;

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
