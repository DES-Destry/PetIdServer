using AutoMapper;
using PetIdServer.Application.Domain.Pet.Commands.Update;
using PetIdServer.Application.Domain.Tag.Dto;
using PetIdServer.Application.Domain.TagReport.Dto;
using PetIdServer.Application.Domain.User.Commands.Update;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;
using PetIdServer.Core.Domain.User;

namespace PetIdServer.Application.Common.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TagEntity, TagReviewForAdminDto>();
        CreateMap<TagEntity, TagDto>()
            .ForMember(dto => dto.Code, expression => expression.MapFrom(entity => entity.PrivateCode))
            .ReverseMap()
            .ForMember(entity => entity.PrivateCode, expression => expression.MapFrom(dto => dto.Code));

        CreateMap<UpdateUserCommand, UserEntity>();
        CreateMap<UpdatePetCommand, PetEntity>();

        CreateMap<TagReportEntity, TagReportShortDto>();
    }
}
