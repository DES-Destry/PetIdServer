namespace PetIdServer.Application.Services;

public interface IPasswordService
{
    Task<string> HashPassword(string password);
    Task<bool> ValidatePassword(string password, string hash);
}
