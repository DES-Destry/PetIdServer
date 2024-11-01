using AutoMapper;
using PetIdServer.Application.Domain.Admin.Commands.Login;
using PetIdServer.Application.Domain.Owner.Commands.Login;
using PetIdServer.Application.Domain.Owner.Commands.Registration;
using PetIdServer.Application.Domain.Tag.Commands.CreateBatch;
using PetIdServer.RestApi.Endpoints.Dto.Admin;
using PetIdServer.RestApi.Endpoints.Dto.Owner;

namespace PetIdServer.RestApi.Mapper;

public class RestApiMappingProfile : Profile
{
    public RestApiMappingProfile()
    {
        CreateMap<LoginAdminDto, LoginAdminCommand>();
        CreateMap<CreateTagsDto, CreateTagsBatchCommand>();

        CreateMap<CreateOwnerDto, RegistrationOwnerCommand>();
        CreateMap<LoginOwnerDto, LoginOwnerCommand>();
    }
}
