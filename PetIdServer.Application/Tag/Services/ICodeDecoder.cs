namespace PetIdServer.Application.Tag.Services;

public interface ICodeDecoder
{
    Task<string> EncodePublicCode(string publicCode);
    Task<string> GetPublicCodeOriginal(string privateCode);
}
