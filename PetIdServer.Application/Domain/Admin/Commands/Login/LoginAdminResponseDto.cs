using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.Admin.Commands.Login;

public class LoginAdminResponseDto : SingleTokenDto
{
    public string AdminId { get; set; }
}
