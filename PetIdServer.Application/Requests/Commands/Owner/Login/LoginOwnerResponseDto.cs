using PetIdServer.Application.Dto;
using PetIdServer.Core.Entities.Id;

namespace PetIdServer.Application.Requests.Commands.Owner.Login;

public class LoginOwnerResponseDto : TokenPairDto
{
    public string OwnerId { get; set; }
}