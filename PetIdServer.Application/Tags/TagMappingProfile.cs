using AutoMapper;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.Tags;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<Tag, TagReviewForAdminDto>();
        CreateMap<Tag, TagDto>()
            .ForMember(dto => dto.Code, expression => expression.MapFrom(entity => entity.PrivateCode))
            .ReverseMap()
            .ForMember(entity => entity.PrivateCode, expression => expression.MapFrom(dto => dto.Code));
    }
}
