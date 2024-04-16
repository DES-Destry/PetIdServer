using AutoMapper;
using PetIdServer.Application.AppDomain.AdminDomain.Commands.Login;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;
using PetIdServer.Application.AppDomain.TagDomain.Commands.CreateBatch;
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
