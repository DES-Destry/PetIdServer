using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.Core.Domain.Admin;

namespace PetIdServer.Application.Common.Services;

public interface IAdminTokenService
{
    Task<string> GenerateToken(AdminEntity admin);
    Task<AdminDto> DecryptAdmin(string token);
}
