using AutoMapper;
using PetIdServer.Application.Tag.Commands.CreateBatch;
using PetIdServer.Application.User.Commands.Login;
using PetIdServer.Application.User.Commands.Registration;
using PetIdServer.RestApi.Endpoints.Dto.Admin;
using PetIdServer.RestApi.Endpoints.Dto.User;

namespace PetIdServer.RestApi.Mapper;

public class RestApiMappingProfile : Profile
{
    public RestApiMappingProfile()
    {
        CreateMap<CreateTagsDto, CreateTagsBatchCommand>();

        CreateMap<CreateUserDto, RegistrationCommand>();
        CreateMap<LoginUserDto, LoginCommand>();
    }
}
