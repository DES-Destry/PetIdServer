using AutoMapper;
using PetIdServer.Application.Tag.Dto;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Tag;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<TagEntity, TagReviewForAdminDto>();
        CreateMap<TagEntity, TagDto>()
            .ForMember(dto => dto.Code, expression => expression.MapFrom(entity => entity.PrivateCode))
            .ReverseMap()
            .ForMember(entity => entity.PrivateCode, expression => expression.MapFrom(dto => dto.Code));
    }
}
