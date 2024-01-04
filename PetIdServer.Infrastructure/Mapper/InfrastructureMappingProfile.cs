using AutoMapper;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Mapper;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<OwnerModel, OwnerEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new OwnerId(model.Email)))
            .ReverseMap()
            .ForMember(model => model.Email,
                expression => expression.MapFrom(domain => domain.Id.Value));

        CreateMap<OwnerContactModel, OwnerContactVo>().ReverseMap();
        CreateMap<PetModel, PetEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new PetId(model.Id)))
            .ReverseMap()
            .ForMember(model => model.Id,
                expression => expression.MapFrom(domain => domain.Id.Value));

        CreateMap<TagModel, TagEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => new TagId(model.Id)))
            .ReverseMap()
            .ForMember(model => model.Id,
                expression => expression.MapFrom(domain => domain.Id.Value));

        CreateMap<AdminModel, AdminEntity>()
            .ForCtorParam("id",
                expression => expression.MapFrom(model => new AdminId(model.Username)))
            .ReverseMap()
            .ForMember(model => model.Username,
                expression => expression.MapFrom(domain => domain.Id.Value));
    }
}
