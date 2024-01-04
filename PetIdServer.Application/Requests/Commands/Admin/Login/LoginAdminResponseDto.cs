using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Admin.Login;

public class LoginAdminResponseDto : SingleTokenDto
{
    public string AdminId { get; set; }
}
