using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Repositories;

public interface ITagRepository
{
    Task<bool> IsIdsAvailable(IEnumerable<int> ids);
    Task<bool> IsCodesAvailable(IEnumerable<string> codes);
    Task<IEnumerable<Tag>> GetAllTags();
    Task<Tag?> GetTagById(TagId id);
    Task<Tag?> GetByCode(string code);
    Task<Tag?> GetByControlCode(long controlCode);

    Task<Tag?> CreateTag(Tag tag);
    Task CreateTagsBatch(IEnumerable<Tag> tags);
    Task AttachPet(TagId id, Pet pet);
}
