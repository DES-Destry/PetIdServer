using System.Diagnostics.CodeAnalysis;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Users.Dto;

public class UserDto
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
    public string? Address { get; init; }
    public string? Description { get; init; }
    public required IEnumerable<UserContact> Contacts { get; init; }

    [return: NotNullIfNotNull("user")]
    public static implicit operator UserDto?(User? user) => user is null
        ? null
        : new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Address = user.Address,
            Description = user.Description,
            Contacts = user.Contacts
        };
}
