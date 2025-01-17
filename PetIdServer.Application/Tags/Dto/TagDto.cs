using PetIdServer.Application.Pets.Dto;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.Tags.Dto;

public record TagDto(int Id, string Code, PetDto? Pet, bool IsAlreadyInUse, DateTime CreatedAt)
{
    public static TagDto FromEmptyTag(Tag tag) => new(tag.Id, tag.PrivateCode, null, tag.IsAlreadyInUse, tag.CreatedAt);
    public static TagDto FromTagWithPet(Tag tag, Pet pet) => new(tag.Id, tag.PrivateCode, pet, tag.IsAlreadyInUse, tag.CreatedAt);
}
