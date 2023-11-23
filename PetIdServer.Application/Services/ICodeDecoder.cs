namespace PetIdServer.Application.Services;

public interface ICodeDecoder
{
    Task<string> EncodePublicCode(string publicCode);
    Task<string> GetPublicCodeOriginal(string privateCode);
}