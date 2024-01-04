using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.Services.Dto;

public class PetDto
{
    public string Type { get; set; }
    public string Name { get; set; }
    public bool Sex { get; set; }
    public bool IsCastrated { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }

    public static implicit operator PetDto(Pet pet)
    {
        return new PetDto
        {
            Type = pet.Type,
            Name = pet.Name,
            Sex = pet.Sex,
            IsCastrated = pet.IsCastrated,
            Photo = pet.Photo,
            Description = pet.Description
        };
    }
}
