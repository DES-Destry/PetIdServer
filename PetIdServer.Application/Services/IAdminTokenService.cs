using PetIdServer.Application.Services.Dto;
using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Services;

public interface IAdminTokenService
{
    Task<string> GenerateToken(Admin admin);
    Task<AdminDto> DecryptAdmin(string token);
}