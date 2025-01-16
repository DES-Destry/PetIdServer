using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Users;
using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;
using PetIdServer.Persistence.Mapper;

namespace PetIdServer.Persistence.Repositories;

public class UserRepository(PetIdContext database) : IUserRepository
{
    public async Task<User?> GetUserById(UserId id)
    {
        UserEntity? entity = await database.Users.AsNoTracking()
            .SingleOrDefaultAsync(user => user.Id == id);
        return entity?.ToCore();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        UserEntity? entity = await database.Users.AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email == email);
        return entity?.ToCore();
    }

    public async Task CreateUser(User user)
    {
        UserEntity entity = user.ToEntity();
        database.Entry(entity).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        UserEntity incomingData = user.ToEntity();
        UserEntity? entity =
            await database.Users.SingleOrDefaultAsync(userModel => userModel.Id == user.Id);

        if (entity is null)
        {
            return;
        }

        database.Entry(entity).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
