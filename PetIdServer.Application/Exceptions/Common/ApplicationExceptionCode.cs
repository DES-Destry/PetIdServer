namespace PetIdServer.Application.Exceptions.Common;

public static class ApplicationExceptionCode
{
    public const string Scope = "APP.";

    public const string Misconfiguration = Scope + "MISCONFIGURATION";
    public const string MomIsBitch = Scope + "MOM_IS_BITCH";
    public const string SomethingWentWrong = Scope + "SOMETHING_WENT_WRONG";
}