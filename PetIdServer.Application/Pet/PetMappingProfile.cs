using AutoMapper;
using PetIdServer.Application.Pet.Commands.Update;
using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.Pet;

public class PetMappingProfile : Profile
{
    public PetMappingProfile()
    {
        CreateMap<UpdatePetCommand, PetEntity>();
    }
}
