using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.AppDomain.OwnerDomain;
using PetIdServer.Core.Domain.Owner;

namespace PetIdServer.Infrastructure.Database.Domain.Owner;

public class OwnerRepository(IMapper mapper, PetIdContext database) : IOwnerRepository
{
    public async Task<OwnerEntity?> GetOwnerById(OwnerId id)
    {
        OwnerModel? model = await database.Owners.AsNoTracking()
            .FirstOrDefaultAsync(owner => owner.Id == id);
        return model is null ? null : mapper.Map<OwnerModel, OwnerEntity>(model);
    }

    public async Task<OwnerEntity?> GetOwnerByEmail(string email)
    {
        OwnerModel? model = await database.Owners.AsNoTracking()
            .FirstOrDefaultAsync(owner => owner.Email == email);
        return model is null ? null : mapper.Map<OwnerModel, OwnerEntity>(model);
    }

    public async Task CreateOwner(OwnerEntity owner)
    {
        OwnerModel? model = mapper.Map<OwnerEntity, OwnerModel>(owner);
        database.Entry(model).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdateOwner(OwnerId id, OwnerEntity owner)
    {
        OwnerModel? incomingData = mapper.Map<OwnerEntity, OwnerModel>(owner);
        OwnerModel? model =
            await database.Owners.FirstOrDefaultAsync(ownerModel => ownerModel.Id == id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
