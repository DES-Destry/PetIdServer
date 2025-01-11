using PetIdServer.Application.Pet.Dto;

namespace PetIdServer.Application.Tag.Dto;

public record CheckTagDto(int Id, CheckPetDto? Pet, bool IsFree);
