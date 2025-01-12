using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Users;
using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class UserRepository(IMapper mapper, PetIdContext database) : IUserRepository
{
    public async Task<User?> GetUserById(UserId id)
    {
        UserEntity? model = await database.Users.AsNoTracking()
            .SingleOrDefaultAsync(user => user.Id == id);
        return model is null ? null : mapper.Map<UserEntity, User>(model);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        UserEntity? model = await database.Users.AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email == email);
        return model is null ? null : mapper.Map<UserEntity, User>(model);
    }

    public async Task CreateUser(User user)
    {
        UserEntity? model = mapper.Map<User, UserEntity>(user);
        database.Entry(model).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        UserEntity? incomingData = mapper.Map<User, UserEntity>(user);
        UserEntity? model =
            await database.Users.SingleOrDefaultAsync(userModel => userModel.Id == user.Id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
