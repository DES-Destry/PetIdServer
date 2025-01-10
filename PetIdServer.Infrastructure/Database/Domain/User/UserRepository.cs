using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.Domain.User;
using PetIdServer.Core.Domain.User;

namespace PetIdServer.Infrastructure.Database.Domain.User;

public class UserRepository(IMapper mapper, PetIdContext database) : IUserRepository
{
    public async Task<UserEntity?> GetUserById(UserId id)
    {
        UserModel? model = await database.Users.AsNoTracking()
            .SingleOrDefaultAsync(user => user.Id == id);
        return model is null ? null : mapper.Map<UserModel, UserEntity>(model);
    }

    public async Task<UserEntity?> GetUserByEmail(string email)
    {
        UserModel? model = await database.Users.AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email == email);
        return model is null ? null : mapper.Map<UserModel, UserEntity>(model);
    }

    public async Task CreateUser(UserEntity user)
    {
        UserModel? model = mapper.Map<UserEntity, UserModel>(user);
        database.Entry(model).State = EntityState.Added;
        await database.SaveChangesAsync();
    }

    public async Task UpdateUser(UserEntity user)
    {
        UserModel? incomingData = mapper.Map<UserEntity, UserModel>(user);
        UserModel? model =
            await database.Users.SingleOrDefaultAsync(userModel => userModel.Id == user.Id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
