using AutoMapper;
using PetIdServer.Application.Pets.Commands.Update;
using PetIdServer.Core.Pets;

namespace PetIdServer.Application.Pets;

public class PetMappingProfile : Profile
{
    public PetMappingProfile()
    {
        CreateMap<UpdatePetCommand, Pet>();
    }
}
