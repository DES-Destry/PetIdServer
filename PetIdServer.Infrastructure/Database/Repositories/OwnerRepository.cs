using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class OwnerRepository(IMapper mapper, PetIdContext database) : IOwnerRepository
{
    public async Task<Owner?> GetOwnerById(OwnerId id)
    {
        var model = await database.Owners.AsNoTracking()
            .FirstOrDefaultAsync(owner => owner.Email == id.Value);
        return model is null ? null : mapper.Map<OwnerModel, Owner>(model);
    }

    public async Task<Owner?> GetOwnerByEmail(string email)
    {
        var model = await database.Owners.AsNoTracking()
            .FirstOrDefaultAsync(owner => owner.Email == email);
        return model is null ? null : mapper.Map<OwnerModel, Owner>(model);
    }

    public async Task<Owner?> CreateOwner(Owner owner)
    {
        var model = mapper.Map<Owner, OwnerModel>(owner);
        var saved = await database.Owners.AddAsync(model);
        await database.SaveChangesAsync();

        return mapper.Map<OwnerModel, Owner>(saved.Entity);
    }

    public async Task UpdateOwner(OwnerId id, Owner owner)
    {
        var incomingData = mapper.Map<Owner, OwnerModel>(owner);
        var model =
            await database.Owners.FirstOrDefaultAsync(ownerModel => ownerModel.Email == id.Value);

        if (model is null) return;

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
