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
            .FirstOrDefaultAsync(petModel => petModel.Id == id.Value);
        return model is null ? null : mapper.Map<PetModel, PetEntity>(model);
    }

    public async Task<PetEntity?> CreatePet(PetEntity pet)
    {
        var model = mapper.Map<PetEntity, PetModel>(pet);
        var saved = await database.Pets.AddAsync(model);

        return mapper.Map<PetModel, PetEntity>(saved.Entity);
    }

    public async Task UpdatePet(PetId id, PetEntity pet)
    {
        var incomingData = mapper.Map<PetEntity, PetModel>(pet);
        var model = await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == id.Value);

        if (model is null) return;

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
