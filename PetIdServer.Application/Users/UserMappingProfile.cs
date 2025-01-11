using AutoMapper;
using PetIdServer.Application.Users.Commands.Update;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UpdateUserCommand, User>();
    }
}
