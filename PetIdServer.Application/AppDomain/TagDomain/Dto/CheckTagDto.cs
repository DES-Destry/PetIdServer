using PetIdServer.Application.AppDomain.PetDomain.Dto;

namespace PetIdServer.Application.AppDomain.TagDomain.Dto;

public record CheckTagDto(int Id, CheckPetDto? Pet, bool IsFree);
