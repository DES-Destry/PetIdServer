using PetIdServer.Application.Services.Dto;
using PetIdServer.Core.Domain.Admin;

namespace PetIdServer.Application.Services;

public interface IAdminTokenService
{
    Task<string> GenerateToken(Admin admin);
    Task<AdminDto> DecryptAdmin(string token);
}
