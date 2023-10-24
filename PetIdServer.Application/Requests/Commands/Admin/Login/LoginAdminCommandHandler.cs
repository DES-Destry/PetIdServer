using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Exceptions.Auth;

namespace PetIdServer.Application.Requests.Commands.Admin.Login;

public class LoginAdminCommandHandler : IRequestHandler<LoginAdminCommand, LoginAdminResponseDto>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IAdminTokenService _adminTokenService;
    private readonly IPasswordService _passwordService;

    public LoginAdminCommandHandler(IAdminRepository adminRepository, IAdminTokenService adminTokenService, IPasswordService passwordService)
    {
        _adminRepository = adminRepository;
        _adminTokenService = adminTokenService;
        _passwordService = passwordService;
    }

    public async Task<LoginAdminResponseDto> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
    {
        // try find admin
        var adminCandidate = await _adminRepository.GetAdminByUsername(request.Username);
        
        if (adminCandidate is null)
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Username}",
                new {request.Username, userType = nameof(Core.Entities.Admin)});
        
        // validate password
        if (adminCandidate.Password != null && !await _passwordService.ValidatePassword(request.Password, adminCandidate.Password))
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Username}",
                new {request.Username, userType = nameof(Core.Entities.Admin)});
        
        // generate tokens
        var token = await _adminTokenService.GenerateToken(adminCandidate);
        return new LoginAdminResponseDto {AccessToken = token, AdminId = adminCandidate.Id.Value};
    }
}