using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.Application.Domain.Tag.Dto;

public record TagDto(int Id, string Code, PetDto Pet, bool IsAlreadyInUse, DateTime CreatedAt);
