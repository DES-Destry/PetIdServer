using AutoMapper;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Update;
using PetIdServer.Application.AppDomain.PetDomain.Commands.Update;
using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Application.AppDomain.TagReportDomain.Dto;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.Common.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TagEntity, TagReviewForAdminDto>();

        CreateMap<UpdateOwnerCommand, OwnerEntity>().ForCtorParam("id",
            expression => expression.MapFrom(model => model.Id));
        ;
        CreateMap<UpdatePetCommand, PetEntity>();

        CreateMap<TagReportEntity, TagReportShortDto>();
    }
}
