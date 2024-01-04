using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.AdminDomain.Commands.Login;

public class LoginAdminResponseDto : SingleTokenDto
{
    public string AdminId { get; set; }
}
