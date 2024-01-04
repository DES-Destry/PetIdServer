namespace PetIdServer.Application.AppDomain.TagDomain.Dto;

public record TagReviewForAdminDto(int Id, bool IsAlreadyInUse, DateTime CreatedAt);
