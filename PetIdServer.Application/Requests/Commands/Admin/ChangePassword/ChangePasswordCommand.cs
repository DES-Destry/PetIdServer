using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Admin.ChangePassword;

public class ChangePasswordCommand : IRequest<SingleTokenDto>
{
    public string Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}