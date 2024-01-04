using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;
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

    public async Task<IEnumerable<TagEntity>> GetAllTags()
    {
        var models = await database.Tags.OrderBy(tag => tag.Id).AsNoTracking().ToListAsync();
        return models.Select(mapper.Map<TagModel, TagEntity>);
    }

    public async Task<TagEntity?> GetTagById(TagId id)
    {
        var model = await database.Tags.AsNoTracking()
            .FirstOrDefaultAsync(tag => tag.Id == id.Value);
        return model is null ? null : mapper.Map<TagModel, TagEntity>(model);
    }

    public async Task<TagEntity?> CreateTag(TagEntity tag)
    {
        var model = mapper.Map<TagEntity, TagModel>(tag);
        var saved = await database.Tags.AddAsync(model);

        return mapper.Map<TagModel, TagEntity>(saved.Entity);
    }

    public async Task<TagEntity?> GetByCode(string code)
    {
        var model = await database.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.Code == code);
        return model is null ? null : mapper.Map<TagModel, TagEntity>(model);
    }

    public async Task<TagEntity?> GetByControlCode(long controlCode)
    {
        var model = await database.Tags
            .Include(tag => tag.Pet)
            .ThenInclude(pet => pet!.Owner)
            .FirstOrDefaultAsync(tag => tag.ControlCode == controlCode);

        return model is null ? null : mapper.Map<TagModel, TagEntity>(model);
    }

    public async Task CreateTagsBatch(IEnumerable<TagEntity> tags)
    {
        var models = tags.Select(mapper.Map<TagEntity, TagModel>);
        await database.Tags.AddRangeAsync(models);
        await database.SaveChangesAsync();
    }

    public async Task AttachPet(TagId id, PetEntity pet)
    {
        var model = await database.Tags.FirstOrDefaultAsync(tag => tag.Id == id.Value);
        if (model is null) return;

        var petModel =
            await database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == pet.Id.Value);
        if (petModel is null) return;

        model.Pet = petModel;

        await database.SaveChangesAsync();
    }
}
