using PetIdServer.Application.Dto.Pet;

namespace PetIdServer.Application.Dto.Tag;

public record CheckTagDto(CheckPetDto? pet, bool isVirgin);