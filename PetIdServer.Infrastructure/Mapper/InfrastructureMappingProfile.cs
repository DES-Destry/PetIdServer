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
            .ReverseMap()
            .ForMember(model => model.Email, expression => expression.MapFrom(domain => domain.Id.Value));

        CreateMap<OwnerContactModel, OwnerContact>().ReverseMap();
        CreateMap<PetModel, Pet>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new PetId(model.Id)))
            .ReverseMap()
            .ForMember(model => model.Id, expression => expression.MapFrom(domain => domain.Id.Value));

        CreateMap<TagModel, Tag>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new TagId(model.Id)))
            .ReverseMap()
            .ForMember(model => model.Id, expression => expression.MapFrom(domain => domain.Id.Value));

        CreateMap<AdminModel, Admin>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new AdminId(model.Username)))
            .ReverseMap()
            .ForMember(model => model.Username, expression => expression.MapFrom(domain => domain.Id.Value));
    }
}