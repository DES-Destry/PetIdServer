using AutoMapper;
using PetIdServer.Application.Dto.Tag;
using PetIdServer.Application.Requests.Commands.Owner.Update;
using PetIdServer.Application.Requests.Commands.Pet.Update;
using PetIdServer.Core.Domains.Owner;
using PetIdServer.Core.Domains.Pet;
using PetIdServer.Core.Domains.Tag;

namespace PetIdServer.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tag, TagReviewForAdminDto>()
            .ForMember(dto => dto.Id, expression => expression.MapFrom(tag => tag.Id.Value));

        CreateMap<UpdateOwnerCommand, Owner>();
        CreateMap<UpdatePetCommand, Pet>();
    }
}