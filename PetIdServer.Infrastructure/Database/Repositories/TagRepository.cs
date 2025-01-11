using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetIdServer.Application.Tags;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags;
using PetIdServer.Infrastructure.Database.Entities;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class TagRepository(IMapper mapper, PetIdContext database) : ITagRepository
{
    public async Task<bool> AreIdsAvailable(IEnumerable<int> ids) => await database.Tags.AnyAsync(tag => ids.Contains(tag.Id));


    public async Task<bool> AreHashCodesAvailable(IEnumerable<string> hashCodes) =>
        await database.Tags.AnyAsync(tag => hashCodes.Contains(tag.HashCode));


    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        List<TagEntity> models = await database.Tags.OrderBy(tag => tag.Id).AsNoTracking().ToListAsync();
        return models.Select(mapper.Map<TagEntity, Tag>);
    }

    public async Task<Tag?> GetTagById(TagId id)
    {
        TagEntity? model = await database.Tags.AsNoTracking()
            .FirstOrDefaultAsync(tag => tag.Id == id);
        return model is null ? null : mapper.Map<TagEntity, Tag>(model);
    }

    public async Task<Tag?> CreateTag(Tag tag)
    {
        TagEntity? model = mapper.Map<Tag, TagEntity>(tag);
        EntityEntry<TagEntity> saved = await database.Tags.AddAsync(model);

        return mapper.Map<TagEntity, Tag>(saved.Entity);
    }

    public async Task<Tag?> GetTagByHashCode(string hashCode)
    {
        TagEntity? model = await database.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.HashCode == hashCode);
        return model is null ? null : mapper.Map<TagEntity, Tag>(model);
    }

    public async Task<Tag?> GetTagByControlCode(long controlCode)
    {
        TagEntity? model = await database.Tags
            .Include(tag => tag.Pet)
            .ThenInclude(pet => pet!.User)
            .FirstOrDefaultAsync(tag => tag.ControlCode == controlCode);

        return model is null ? null : mapper.Map<TagEntity, Tag>(model);
    }

    public async Task CreateTagsBatch(IEnumerable<Tag> tags)
    {
        IEnumerable<TagEntity> models = tags.Select(mapper.Map<Tag, TagEntity>);
        await database.Tags.AddRangeAsync(models);
        await database.SaveChangesAsync();
    }

    public async Task AttachPet(TagId id, Pet pet)
    {
        TagEntity? model = await database.Tags.FirstOrDefaultAsync(tag => tag.Id == id);

        if (model is null)
        {
            return;
        }

        PetEntity? petModel =
            await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == pet.Id);

        if (petModel is null)
        {
            return;
        }

        model.Pet = petModel;

        await database.SaveChangesAsync();
    }

    public async Task UpdateTag(TagId id, Tag pet)
    {
        TagEntity? incomingData = mapper.Map<Tag, TagEntity>(pet);
        TagEntity? model = await database.Tags.FirstOrDefaultAsync(tagModel => tagModel.Id == id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
