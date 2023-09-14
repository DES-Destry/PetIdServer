using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class TagRepository : ITagRepository
{
    private readonly IMapper _mapper;
    private readonly PetIdContext _database;

    public TagRepository(IMapper mapper, PetIdContext database)
    {
        _mapper = mapper;
        _database = database;
    }

    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        var models = await _database.Tags.AsNoTracking().ToListAsync();
        return models.Select(model => _mapper.Map<TagModel, Tag>(model));
    }

    public async Task<Tag?> GetTagById(int id)
    {
        var model = await _database.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.Id == id);
        return model is null ? null : _mapper.Map<TagModel, Tag>(model);
    }

    public async Task<Tag?> CreateTag(Tag tag)
    {
        var model = _mapper.Map<Tag, TagModel>(tag);
        var saved = await _database.Tags.AddAsync(model);

        return _mapper.Map<TagModel, Tag>(saved.Entity);
    }

    public async Task AttachPet(int tagId, Pet pet)
    {
        var model = await _database.Tags.FirstOrDefaultAsync(tag => tag.Id == tagId);
        if (model is null) return;

        var petModel = await _database.Pets.FirstOrDefaultAsync(petModel => petModel.Id == pet.Id);
        if (petModel is null) return;

        model.Pet = petModel;
        
        await _database.SaveChangesAsync();
    }
}