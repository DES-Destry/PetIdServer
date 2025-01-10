using PetIdServer.Core.Domain.User;

namespace PetIdServer.Application.Common.Services.Dto;

public class UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string Name { get; init; }
    public string? Address { get; init; }
    public string? Description { get; init; }
    public IList<UserContactVo> Contacts { get; init; }
    public IList<PetDto> Pets { get; init; }

    public static implicit operator UserDto(UserEntity user)
    {
        return new UserDto
        {
            Email = user.Email,
            Name = user.Name,
            Address = user.Address,
            Description = user.Description,
            Contacts = user.Contacts,
            Pets = user.Pets.Select(pet => (PetDto)pet).ToList()
        };
    }
}
