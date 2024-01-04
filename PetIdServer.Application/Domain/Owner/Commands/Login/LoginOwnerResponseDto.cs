using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.Owner.Commands.Login;

public class LoginOwnerResponseDto : TokenPairDto
{
    public string OwnerId { get; set; }
}
