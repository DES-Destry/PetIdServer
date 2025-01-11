namespace PetIdServer.Application.Tags.Services;

public interface ICodeDecoder
{
    Task<string> EncodePublicCode(string publicCode);
    Task<string> GetPublicCodeOriginal(string privateCode);
}
