namespace PetIdServer.Application.Dto;

public class VoidResponseDto
{
    public bool IsExecuted { get; set; }

    public static VoidResponseDto Executed => new VoidResponseDto {IsExecuted = true};
    public static VoidResponseDto NotExecuted => new VoidResponseDto {IsExecuted = false};
}