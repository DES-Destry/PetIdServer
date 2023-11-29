namespace PetIdServer.Application.Dto.Tag;

public class TagForAdminDto
{
    public int Id { get; set; }
    public string PublicCode { get; set; }
    public long ControlCode { get; set; }
    public bool IsAlreadyInUse { get; set; }
}