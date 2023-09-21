using PetIdServer.Core.Entities;
using PetIdServer.Core.Entities.Id;

namespace PetIdServer.Application.Repositories;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllTags();
    Task<Tag?> GetTagById(TagId id);

    Task<Tag?> CreateTag(Tag tag);
    Task AttachPet(TagId id, Pet pet);
}