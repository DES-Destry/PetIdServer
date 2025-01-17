using AutoMapper;
using PetIdServer.Core.Tags;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public class DatabaseMappingProfile : Profile
{
    public DatabaseMappingProfile()
    {
        CreateMap<TagEntity, Tag>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (TagId)model.Id))
            .ForMember(entity => entity.PrivateCode, expression => expression.MapFrom(model => model.Code))
            .ReverseMap()
            .ForMember(model => model.Id,
                       expression => expression.MapFrom(domain => domain.Id))
            .ForMember(model => model.Code, expression => expression.MapFrom(entity => entity.PrivateCode));
    }
}
