namespace PetIdServer.Infrastructure.Exceptions;

public static class InfrastructureExceptionCode
{
    public const string Scope = "INFRA.";

    public const string Misconfiguration = Scope + "MISCONFIGURATION";
    public const string MomIsBitch = Scope + "MOM_IS_BITCH";
    public const string SomethingWentWrong = Scope + "SOMETHING_WENT_WRONG";
}
