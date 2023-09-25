namespace PetIdServer.Application.Exceptions;

public static class ApplicationExceptionCode
{
    public const string DefaultScope = "APP.";

    public const string Misconfiguration = DefaultScope + "MISCONFIGURATION";
    public const string SomethingWentWrong = DefaultScope + "SOMETHING_WENT_WRONG";
}