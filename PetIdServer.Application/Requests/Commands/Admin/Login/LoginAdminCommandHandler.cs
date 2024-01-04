using MediatR;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Common.Exceptions.Auth;

namespace PetIdServer.Application.Requests.Commands.Admin.Login;

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
                new {request.Username, userType = nameof(Core.Domain.Admin.Admin)});

        // validate password
        if (adminCandidate.Password != null &&
            !await passwordService.ValidatePassword(request.Password, adminCandidate.Password))
            throw new IncorrectCredentialsException(
                $"Incorrect credentials for: {request.Username}",
                new {request.Username, userType = nameof(Core.Domain.Admin.Admin)});

        // generate tokens
        var token = await adminTokenService.GenerateToken(adminCandidate);
        return new LoginAdminResponseDto {AccessToken = token, AdminId = adminCandidate.Id.Value};
    }
}
