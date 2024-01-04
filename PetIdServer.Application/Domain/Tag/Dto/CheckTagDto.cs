using PetIdServer.Application.Domain.Pet.Dto;

namespace PetIdServer.Application.Domain.Tag.Dto;

public record CheckTagDto(int Id, CheckPetDto? Pet, bool IsFree);
