using PetIdServer.Application.Pets.Dto;

namespace PetIdServer.Application.Tags.Dto;

public record TagDto(int Id, string Code, PetDto Pet, bool IsAlreadyInUse, DateTime CreatedAt);
