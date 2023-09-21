using AutoMapper;
using PetIdServer.Application.Requests.Commands.Admin.Login;
using PetIdServer.RestApi.Endpoints.Dto.Admin;

namespace PetIdServer.RestApi.Mapper;

public class RestApiMappingProfile : Profile
{
    public RestApiMappingProfile()
    {
        CreateMap<LoginAdminDto, LoginAdminCommand>();
    }
}