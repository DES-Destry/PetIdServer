using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.Owner.Commands.Login;

public class LoginOwnerResponseDto : TokenPairDto
{
    public Guid OwnerId { get; set; }
}
