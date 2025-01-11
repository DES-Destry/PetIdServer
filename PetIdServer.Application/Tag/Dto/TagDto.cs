using PetIdServer.Application.Pet.Dto;

namespace PetIdServer.Application.Tag.Dto;

public record TagDto(int Id, string Code, PetDto Pet, bool IsAlreadyInUse, DateTime CreatedAt);
