namespace PetIdServer.Core.Exceptions;

public static class CoreExceptionCode
{
    public const string DefaultScope = "CORE.";

    public const string IncorrectCredentials = DefaultScope + "INCORRECT_CREDENTIALS";

    public const string OwnerNotFound = DefaultScope + "OWNER_NOT_FOUND";
    public const string OwnerAlreadyRegistered = DefaultScope + "OWNER_ALREADY_REGISTERED";

    public const string PetNotFound = DefaultScope + "PET_NOT_FOUND";

    public const string TagNotFound = DefaultScope + "TAG_NOT_FOUND";
    public const string TagAlreadyInUse = DefaultScope + "TAG_ALREADY_IN_USE";
}