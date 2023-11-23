namespace PetIdServer.RestApi.Endpoints.Dto.Admin;

public class CreateTagsDto
{
    public int FromId { get; set; }
    public int ToId { get; set; }
    public IEnumerable<string> Codes { get; set; }
}