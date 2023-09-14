using PetIdServer.Application.Dto;
using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Services;

public interface IOwnerTokenService
{
    Task<TokenPairDto> GenerateTokens(Owner owner);
    Task<TokenPairDto> RefreshTokens(string refreshToken);

    Task<Owner> DecryptOwner(string accessToken);
}