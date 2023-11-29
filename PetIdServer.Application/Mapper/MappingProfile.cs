using AutoMapper;
using PetIdServer.Application.Dto.Tag;
using PetIdServer.Application.Requests.Commands.Owner.Update;
using PetIdServer.Application.Requests.Commands.Pet.Update;
using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tag, TagReviewForAdminDto>();

        CreateMap<UpdateOwnerCommand, Owner>();
        CreateMap<UpdatePetCommand, Pet>();
    }
}