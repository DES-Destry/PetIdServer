using PetIdServer.Core.Users;

namespace PetIdServer.Application.Users;

public interface IUserRepository
{
    Task<User?> GetUserById(UserId id);
    Task<User?> GetUserByEmail(string email);
    Task CreateUser(User user);

    Task UpdateUser(User user);
}
