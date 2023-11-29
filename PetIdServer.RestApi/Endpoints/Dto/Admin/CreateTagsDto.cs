namespace PetIdServer.RestApi.Endpoints.Dto.Admin;

public class CreateTagsDto
{
    public int IdFrom { get; set; }
    public int IdTo { get; set; }
    public IEnumerable<string> Codes { get; set; }
}