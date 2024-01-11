using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.AdminDomain.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<SingleTokenDto>
{
    public string Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
