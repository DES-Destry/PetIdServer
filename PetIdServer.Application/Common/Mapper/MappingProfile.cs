using AutoMapper;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Update;
using PetIdServer.Application.AppDomain.PetDomain.Commands.Update;
using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Common.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tag, TagReviewForAdminDto>()
            .ForMember(dto => dto.Id, expression => expression.MapFrom(tag => tag.Id));

        CreateMap<UpdateOwnerCommand, Owner>();
        CreateMap<UpdatePetCommand, Pet>();
    }
}
