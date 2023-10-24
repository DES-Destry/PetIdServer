using PetIdServer.Core.Entities;
using PetIdServer.Core.ValueObjects;

namespace PetIdServer.Application.Services.Dto;

public class OwnerDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public IList<OwnerContact> Contacts { get; set; }
    public IList<PetDto> Pets { get; set; }

    public static implicit operator OwnerDto(Owner owner)
    {
        return new OwnerDto
        {
            Email = owner.Email,
            Name = owner.Name,
            Address = owner.Address,
            Description = owner.Description,
            Contacts = owner.Contacts,
            Pets = owner.Pets.Select(pet => (PetDto) pet).ToList(),
        };
    }
}