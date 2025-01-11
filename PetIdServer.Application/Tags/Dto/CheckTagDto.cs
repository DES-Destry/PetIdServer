using PetIdServer.Application.Pets.Dto;

namespace PetIdServer.Application.Tags.Dto;

public record CheckTagDto(int Id, CheckPetDto? Pet, bool IsFree);
