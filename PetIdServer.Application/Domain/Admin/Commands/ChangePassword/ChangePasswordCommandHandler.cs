using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Common.Exceptions.Auth;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Admin.Exceptions;

namespace PetIdServer.Application.Domain.Admin.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IAdminRepository adminRepository,
    IAdminTokenService adminTokenService,
    IHashService hashService)
    : IRequestHandler<ChangePasswordCommand, SingleTokenDto>
{
    public async Task<SingleTokenDto> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        AdminEntity? adminCandidate = await adminRepository.GetAdminByUsername(request.Id);

        if (adminCandidate is null)
        {
            throw new AdminNotFoundException($"Admin {request.Id} not found!",
                                             new
                                             {
                                                 AdminId = request.Id, UseCase = nameof(ChangePasswordCommandHandler)
                                             });
        }

        if (adminCandidate.Password != null &&
            !await hashService.Validate(request.OldPassword, adminCandidate.Password))
        {
            throw new IncorrectCredentialsException($"Incorrect credentials for: {request.Id}",
                                                    new
                                                    {
                                                        Username = request.Id, userType = nameof(AdminEntity)
                                                    });
        }

        string newHashedPassword = await hashService.Hash(request.NewPassword);
        adminCandidate.Password = newHashedPassword;

        await adminRepository.UpdateAdmin(adminCandidate.Id, adminCandidate);

        string token = await adminTokenService.GenerateToken(adminCandidate);
        return new SingleTokenDto
        {
            AccessToken = token
        };
    }
}
