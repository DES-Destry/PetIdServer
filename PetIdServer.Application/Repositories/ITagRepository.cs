using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Repositories;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllTags();
    Task<Tag?> GetTagById(int id);

    Task<Tag?> CreateTag(Tag tag);
    Task AttachPet(int tagId, Pet pet);
}