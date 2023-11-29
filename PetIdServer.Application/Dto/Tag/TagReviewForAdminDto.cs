namespace PetIdServer.Application.Dto.Tag;

public class TagReviewForAdminDto
{
    public int Id { get; set; }

    public bool IsAlreadyInUse { get; set; }

    public DateTime CreatedAt { get; set; }
}