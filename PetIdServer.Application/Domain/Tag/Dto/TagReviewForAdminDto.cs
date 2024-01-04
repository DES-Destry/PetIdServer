namespace PetIdServer.Application.Domain.Tag.Dto;

public record TagReviewForAdminDto(int Id, bool IsAlreadyInUse, DateTime CreatedAt);
