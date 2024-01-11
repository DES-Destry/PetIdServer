using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.Application.Common.Services;

public interface IOwnerTokenService
{
    Task<TokenPairDto> GenerateTokens(OwnerDto owner);
    Task<TokenPairDto> RefreshTokens(string refreshToken);

    Task<OwnerDto> DecryptOwner(string accessToken);
}
