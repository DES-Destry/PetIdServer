using MediatR;
using PetIdServer.Application.Pets;
using PetIdServer.Application.Pets.Dto;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Application.Users;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Tags.Queries.ControlCheck;

public class TagControlCheckQueryHandler(
    ITagRepository tagRepository,
    IPetRepository petRepository,
    IUserRepository userRepository)
    : IRequestHandler<TagControlCheckQuery, CheckTagDto>
{
    public async Task<CheckTagDto> Handle(
        TagControlCheckQuery request,
        CancellationToken cancellationToken)
    {
        Tag tag = await tagRepository.GetTagByControlCode(request.ControlCode) ??
                  throw new TagNotFoundException($"Invalid control code: {request.ControlCode}", new
                  {
                      UseCase = nameof(TagControlCheckQuery),
                      controlCode = request.ControlCode
                  });

        bool isFree = !tag.IsAlreadyInUse;

        Pet? pet = tag.IsAlreadyInUse ? await petRepository.GetPetById(tag.PetId!) : null;
        User? owner = pet is not null ? await userRepository.GetUserById(pet.OwnerId) : null;

        CheckPetDto petInfo = CheckPetDto.FromPetAndHisOwner(pet, owner);

        return new CheckTagDto(tag.Id, petInfo, isFree);
    }
}
