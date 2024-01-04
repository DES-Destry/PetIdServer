namespace PetIdServer.Application.Common.Dto;

public class VoidResponseDto
{
    public bool IsExecuted { get; set; }

    public static VoidResponseDto Executed => new() {IsExecuted = true};
    public static VoidResponseDto NotExecuted => new() {IsExecuted = false};
}
