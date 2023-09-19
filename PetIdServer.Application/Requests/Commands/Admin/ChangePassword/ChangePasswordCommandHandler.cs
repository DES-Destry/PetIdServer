using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Exceptions.Auth;

namespace PetIdServer.Application.Requests.Commands.Admin.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, SingleTokenDto>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IAdminTokenService _adminTokenService;
    private readonly IPasswordService _passwordService;

    public ChangePasswordCommandHandler(IAdminRepository adminRepository, IAdminTokenService adminTokenService, IPasswordService passwordService)
    {
        _adminRepository = adminRepository;
        _adminTokenService = adminTokenService;
        _passwordService = passwordService;
    }

    public async Task<SingleTokenDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var adminCandidate = await _adminRepository.GetAdminByUsername(request.Username);

        if (adminCandidate is null)
            throw new Exception(); // Admin not found

        if (adminCandidate.Password != null &&
            await _passwordService.ValidatePassword(request.OldPassword, adminCandidate.Password))
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Username}",
                new {request.Username, userType = nameof(Core.Entities.Admin)});

        var newHashedPassword = await _passwordService.HashPassword(request.NewPassword);
        adminCandidate.Password = newHashedPassword;

        await _adminRepository.UpdateAdmin(adminCandidate.Id, adminCandidate);

        var token = await _adminTokenService.GenerateToken(adminCandidate);
        return new SingleTokenDto {AccessToken = token};
    }
}