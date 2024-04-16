using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;

public class LoginOwnerResponseDto : TokenPairDto
{
    public Guid OwnerId { get; set; }
}
