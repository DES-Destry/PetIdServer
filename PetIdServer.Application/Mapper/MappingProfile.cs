using AutoMapper;
using PetIdServer.Application.Dto.Tag;
using PetIdServer.Application.Requests.Commands.Owner.Update;
using PetIdServer.Application.Requests.Commands.Pet.Update;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;

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
