using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Common.Exceptions.Auth;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Admin.Exceptions;

namespace PetIdServer.Application.AppDomain.AdminDomain.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IAdminRepository adminRepository,
    IAdminTokenService adminTokenService,
    IPasswordService passwordService)
    : IRequestHandler<ChangePasswordCommand, SingleTokenDto>
{
    public async Task<SingleTokenDto> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var adminCandidate = await adminRepository.GetAdminByUsername(request.Id);

        if (adminCandidate is null)
            throw new AdminNotFoundException($"Admin {request.Id} not found!",
                new {AdminId = request.Id, UseCase = nameof(ChangePasswordCommandHandler)});

        if (adminCandidate.Password != null &&
            !await passwordService.ValidatePassword(request.OldPassword, adminCandidate.Password))
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Id}",
                new {Username = request.Id, userType = nameof(Admin)});

        var newHashedPassword = await passwordService.HashPassword(request.NewPassword);
        adminCandidate.Password = newHashedPassword;

        await adminRepository.UpdateAdmin(adminCandidate.Id, adminCandidate);

        var token = await adminTokenService.GenerateToken(adminCandidate);
        return new SingleTokenDto {AccessToken = token};
    }
}
