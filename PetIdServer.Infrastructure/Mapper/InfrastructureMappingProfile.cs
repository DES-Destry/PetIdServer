using AutoMapper;
using PetIdServer.Core.Entities;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Core.ValueObjects;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Mapper;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<OwnerModel, Owner>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new OwnerId(model.Email)))
            .ReverseMap();
        CreateMap<OwnerContactModel, OwnerContact>().ReverseMap();

        CreateMap<PetModel, Pet>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new PetId(model.Id)))
            .ReverseMap();
        
        CreateMap<TagModel, Tag>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new TagId(model.Id)))
            .ReverseMap();

        CreateMap<AdminModel, Admin>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new AdminId(model.Username)))
            .ReverseMap();
    }
}