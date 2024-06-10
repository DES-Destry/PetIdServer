namespace PetIdServer.RestApi.Endpoints.Dto.Owner;

public class CreateOwnerDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
}
