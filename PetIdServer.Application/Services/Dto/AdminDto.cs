using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Services.Dto;

public class AdminDto
{
    public string Username { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PasswordLastChangedAt { get; set; }

    public bool IsNotCapable { get; set; }

    public static implicit operator AdminDto(Admin admin)
    {
        return new AdminDto
        {
            Username = admin.Username,
            CreatedAt = admin.CreatedAt,
            PasswordLastChangedAt = admin.PasswordLastChangedAt,
            IsNotCapable = admin.IsNotCapable
        };
    }
}