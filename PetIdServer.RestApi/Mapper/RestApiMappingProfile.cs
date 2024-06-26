using AutoMapper;
using PetIdServer.Application.AppDomain.AdminDomain.Commands.Login;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;
using PetIdServer.Application.AppDomain.PetDomain.Commands.Attach;
using PetIdServer.Application.AppDomain.TagDomain.Commands.CreateBatch;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.RestApi.Binding;
using PetIdServer.RestApi.Endpoints.Dto.Admin;
using PetIdServer.RestApi.Endpoints.Dto.Owner;
using PetIdServer.RestApi.Endpoints.Dto.Pet;

namespace PetIdServer.RestApi.Mapper;

public class RestApiMappingProfile : Profile
{
    public RestApiMappingProfile()
    {
        CreateMap<LoginAdminDto, LoginAdminCommand>();
        CreateMap<CreateTagsDto, CreateTagsBatchCommand>();

        CreateMap<RequestOwner, OwnerEntity>().ForCtorParam("id",
            expression => expression.MapFrom(model => model.Id));

        CreateMap<CreateOwnerDto, RegistrationOwnerCommand>();
        CreateMap<LoginOwnerDto, LoginOwnerCommand>();

        CreateMap<AttachPetDto, AttachPetCommand>();
    }
}
