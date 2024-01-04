using PetIdServer.Application.Dto.Pet;

namespace PetIdServer.Application.Dto.Tag;

public record CheckTagDto(int id, CheckPetDto? pet, bool isFree);
