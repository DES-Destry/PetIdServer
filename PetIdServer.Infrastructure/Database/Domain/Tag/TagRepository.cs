using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetIdServer.Application.Domain.Tag;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Infrastructure.Database.Domain.Pet;

namespace PetIdServer.Infrastructure.Database.Domain.Tag;

public class TagRepository(IMapper mapper, PetIdContext database) : ITagRepository
{
    public async Task<bool> AreIdsAvailable(IEnumerable<int> ids) => await database.Tags.AnyAsync(tag => ids.Contains(tag.Id));


    public async Task<bool> AreHashCodesAvailable(IEnumerable<string> hashCodes) =>
        await database.Tags.AnyAsync(tag => hashCodes.Contains(tag.HashCode));


    public async Task<IEnumerable<TagEntity>> GetAllTags()
    {
        List<TagModel> models = await database.Tags.OrderBy(tag => tag.Id).AsNoTracking().ToListAsync();
        return models.Select(mapper.Map<TagModel, TagEntity>);
    }

    public async Task<TagEntity?> GetTagById(TagId id)
    {
        TagModel? model = await database.Tags.AsNoTracking()
            .FirstOrDefaultAsync(tag => tag.Id == id);
        return model is null ? null : mapper.Map<TagModel, TagEntity>(model);
    }

    public async Task<TagEntity?> CreateTag(TagEntity tag)
    {
        TagModel? model = mapper.Map<TagEntity, TagModel>(tag);
        EntityEntry<TagModel> saved = await database.Tags.AddAsync(model);

        return mapper.Map<TagModel, TagEntity>(saved.Entity);
    }

    public async Task<TagEntity?> GetTagByHashCode(string hashCode)
    {
        TagModel? model = await database.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.HashCode == hashCode);
        return model is null ? null : mapper.Map<TagModel, TagEntity>(model);
    }

    public async Task<TagEntity?> GetTagByControlCode(long controlCode)
    {
        TagModel? model = await database.Tags
            .Include(tag => tag.Pet)
            .ThenInclude(pet => pet!.User)
            .FirstOrDefaultAsync(tag => tag.ControlCode == controlCode);

        return model is null ? null : mapper.Map<TagModel, TagEntity>(model);
    }

    public async Task CreateTagsBatch(IEnumerable<TagEntity> tags)
    {
        IEnumerable<TagModel> models = tags.Select(mapper.Map<TagEntity, TagModel>);
        await database.Tags.AddRangeAsync(models);
        await database.SaveChangesAsync();
    }

    public async Task AttachPet(TagId id, PetEntity pet)
    {
        TagModel? model = await database.Tags.FirstOrDefaultAsync(tag => tag.Id == id);

        if (model is null)
        {
            return;
        }

        PetModel? petModel =
            await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == pet.Id);

        if (petModel is null)
        {
            return;
        }

        model.Pet = petModel;

        await database.SaveChangesAsync();
    }

    public async Task UpdateTag(TagId id, TagEntity pet)
    {
        TagModel? incomingData = mapper.Map<TagEntity, TagModel>(pet);
        TagModel? model = await database.Tags.FirstOrDefaultAsync(tagModel => tagModel.Id == id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
