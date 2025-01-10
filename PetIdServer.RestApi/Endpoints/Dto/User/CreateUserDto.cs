namespace PetIdServer.RestApi.Endpoints.Dto.User;

public class CreateUserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
}
