using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class TagRepository(IMapper mapper, PetIdContext database) : ITagRepository
{
    public async Task<bool> IsIdsAvailable(IEnumerable<int> ids)
    {
        var count = database.Tags.Count(tag => ids.Contains(tag.Id));
        var result = count == 0;

        return await Task.FromResult(result);
    }

    public async Task<bool> IsCodesAvailable(IEnumerable<string> codes)
    {
        var count = database.Tags.Count(tag => codes.Contains(tag.Code));
        var result = count == 0;

        return await Task.FromResult(result);
    }
    
    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        var models = await database.Tags.AsNoTracking().ToListAsync();
        return models.Select(mapper.Map<TagModel, Tag>);
    }

    public async Task<Tag?> GetTagById(TagId id)
    {
        var model = await database.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.Id == id.Value);
        return model is null ? null : mapper.Map<TagModel, Tag>(model);
    }

    public async Task<Tag?> GetByCode(string code)
    {
        var model = await database.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.Code == code);
        return model is null ? null : mapper.Map<TagModel, Tag>(model);
    }
    
    public async Task<Tag?> GetByControlCode(long controlCode)
    {
        var model = await database.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.ControlCode == controlCode);
        return model is null ? null : mapper.Map<TagModel, Tag>(model);
    }

    public async Task<Tag?> CreateTag(Tag tag)
    {
        var model = mapper.Map<Tag, TagModel>(tag);
        var saved = await database.Tags.AddAsync(model);

        return mapper.Map<TagModel, Tag>(saved.Entity);
    }

    public async Task CreateTagsBatch(IEnumerable<Tag> tags)
    {
        var models = tags.Select(mapper.Map<Tag, TagModel>);
        await database.Tags.AddRangeAsync(models);
        await database.SaveChangesAsync();
    }

    public async Task AttachPet(TagId id, Pet pet)
    {
        var model = await database.Tags.FirstOrDefaultAsync(tag => tag.Id == id.Value);
        if (model is null) return;

        var petModel = await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == pet.Id.Value);
        if (petModel is null) return;

        model.Pet = petModel;
        
        await database.SaveChangesAsync();
    }
}