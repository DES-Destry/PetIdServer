using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Common.Exceptions.Auth;
using PetIdServer.Core.Domain.Admin;

namespace PetIdServer.Application.AppDomain.AdminDomain.Commands.Login;

public class LoginAdminCommandHandler(
    IAdminRepository adminRepository,
    IAdminTokenService adminTokenService,
    IPasswordService passwordService)
    : IRequestHandler<LoginAdminCommand, LoginAdminResponseDto>
{
    public async Task<LoginAdminResponseDto> Handle(
        LoginAdminCommand request,
        CancellationToken cancellationToken)
    {
        // try find admin
        var adminCandidate = await adminRepository.GetAdminByUsername(request.Username);

        if (adminCandidate is null)
            throw new IncorrectCredentialsException(
                $"Incorrect credentials for: {request.Username}",
                new {request.Username, userType = nameof(Admin)});

        // validate password
        if (adminCandidate.Password != null &&
            !await passwordService.ValidatePassword(request.Password, adminCandidate.Password))
            throw new IncorrectCredentialsException(
                $"Incorrect credentials for: {request.Username}",
                new {request.Username, userType = nameof(Admin)});

        // generate tokens
        var token = await adminTokenService.GenerateToken(adminCandidate);
        return new LoginAdminResponseDto {AccessToken = token, AdminId = adminCandidate.Id};
    }
}
