using AutoMapper;
using PetIdServer.Application.Domain.Admin.Commands.Login;
using PetIdServer.Application.Domain.Tag.Commands.CreateBatch;
using PetIdServer.RestApi.Endpoints.Dto.Admin;

namespace PetIdServer.RestApi.Mapper;

public class RestApiMappingProfile : Profile
{
    public RestApiMappingProfile()
    {
        CreateMap<LoginAdminDto, LoginAdminCommand>();
        CreateMap<CreateTagsDto, CreateTagsBatchCommand>();
    }
}
