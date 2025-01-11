using AutoMapper;
using PetIdServer.Application.User.Commands.Update;
using PetIdServer.Core.Domain.User;

namespace PetIdServer.Application.User;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UpdateUserCommand, UserEntity>();
    }
}
