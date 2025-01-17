using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetIdServer.Application.Tags;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags;
using PetIdServer.Persistence.Entities;
using PetIdServer.Persistence.Mapper;

namespace PetIdServer.Persistence.Repositories;

public class TagRepository(PetIdContext database) : ITagRepository
{
    public async Task<bool> AreIdsAvailable(IEnumerable<int> ids) => await database.Tags.AnyAsync(tag => ids.Contains(tag.Id));


    public async Task<bool> AreHashCodesAvailable(IEnumerable<string> hashCodes) =>
        await database.Tags.AnyAsync(tag => hashCodes.Contains(tag.HashCode));


    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        List<TagEntity> models = await database.Tags.OrderBy(tag => tag.Id).AsNoTracking().ToListAsync();
        return models.Select(TagMapper.ToCore);
    }

    public async Task<Tag?> GetTagById(TagId id)
    {
        TagEntity? tagEntity = await database.Tags.AsNoTracking()
            .FirstOrDefaultAsync(tag => tag.Id == id);
        return tagEntity?.ToCore();
    }

    public async Task<Tag?> GetTagByHashCode(string hashCode)
    {
        TagEntity? tagEntity = await database.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.HashCode == hashCode);
        return tagEntity?.ToCore();
    }

    public async Task<Tag?> GetTagByControlCode(long controlCode)
    {
        TagEntity? tagEntity = await database.Tags
            .Include(tag => tag.Pet)
            .ThenInclude(pet => pet!.User)
            .FirstOrDefaultAsync(tag => tag.ControlCode == controlCode);

        return tagEntity?.ToCore();
    }

    public async Task<Tag?> CreateTag(Tag tag)
    {
        bool idIsFree = await AreIdsAvailable([tag.Id]);

        if (!idIsFree)
        {
            return null;
        }

        TagEntity model = tag.ToEntity();
        EntityEntry<TagEntity> saved = await database.Tags.AddAsync(model);

        return saved.Entity.ToCore();
    }

    public async Task CreateTagsBatch(IEnumerable<Tag> tags)
    {
        IEnumerable<TagEntity> entities = tags.Select(TagMapper.ToEntity);
        await database.Tags.AddRangeAsync(entities);
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

    public async Task UpdateTag(Tag tag)
    {
        TagEntity incomingData = tag.ToEntity();
        TagEntity? tagEntity = await database.Tags.FirstOrDefaultAsync(entity => entity.Id == tag.Id);

        if (tagEntity is null)
        {
            return;
        }

        database.Entry(tagEntity).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
