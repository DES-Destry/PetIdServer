using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Owner.Login;

public class LoginOwnerResponseDto : TokenPairDto
{
    public string OwnerId { get; set; }
}