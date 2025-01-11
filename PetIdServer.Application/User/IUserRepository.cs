using PetIdServer.Core.Domain.User;

namespace PetIdServer.Application.User;

public interface IUserRepository
{
    Task<UserEntity?> GetUserById(UserId id);
    Task<UserEntity?> GetUserByEmail(string email);
    Task CreateUser(UserEntity user);

    Task UpdateUser(UserEntity user);
}
