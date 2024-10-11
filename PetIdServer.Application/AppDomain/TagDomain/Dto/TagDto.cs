using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.Application.AppDomain.TagDomain.Dto;

public record TagDto(int Id, string Code, PetDto Pet, bool IsAlreadyInUse, DateTime CreatedAt);
