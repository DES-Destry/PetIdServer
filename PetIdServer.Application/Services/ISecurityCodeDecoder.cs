namespace PetIdServer.Application.Services;

public interface ISecurityCodeDecoder
{
    Task<string> EncodeSecurityCode(string securityCode);
    Task<string> GetSecurityCodeOriginal(string encoded);
}