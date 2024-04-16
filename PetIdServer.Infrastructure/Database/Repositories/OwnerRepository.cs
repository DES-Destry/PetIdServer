using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.AppDomain.OwnerDomain;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class OwnerRepository(IMapper mapper, PetIdContext database) : IOwnerRepository
{
    public async Task<OwnerEntity?> GetOwnerById(OwnerId id)
    {
        var model = await database.Owners.AsNoTracking()
            .FirstOrDefaultAsync(owner => owner.Id == id);
        return model is null ? null : mapper.Map<OwnerModel, OwnerEntity>(model);
    }

    public async Task<OwnerEntity?> GetOwnerByEmail(string email)
    {
        var model = await database.Owners.AsNoTracking()
            .FirstOrDefaultAsync(owner => owner.Email == email);
        return model is null ? null : mapper.Map<OwnerModel, OwnerEntity>(model);
    }

    public async Task CreateOwner(OwnerEntity owner)
    {
        var model = mapper.Map<OwnerEntity, OwnerModel>(owner);
        database.Entry(model).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdateOwner(OwnerId id, OwnerEntity owner)
    {
        var incomingData = mapper.Map<OwnerEntity, OwnerModel>(owner);
        var model =
            await database.Owners.FirstOrDefaultAsync(ownerModel => ownerModel.Id == id);

        if (model is null) return;

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
