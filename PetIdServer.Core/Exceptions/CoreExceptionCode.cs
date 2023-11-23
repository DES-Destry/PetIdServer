namespace PetIdServer.Core.Exceptions;

public static class CoreExceptionCode
{
    public const string DefaultScope = "CORE.";

    public const string ValidationException = DefaultScope + "VALIDATION_EXCEPTION";

    public const string IncorrectCredentials = DefaultScope + "INCORRECT_CREDENTIALS";

    public const string AccessTokenMalformed = DefaultScope + "ACCESS_TOKEN_MALFORMED";
    public const string RefreshTokenMalformed = DefaultScope + "REFRESH_TOKEN_MALFORMED";

    public const string OwnerNotFound = DefaultScope + "OWNER_NOT_FOUND";
    public const string OwnerAlreadyRegistered = DefaultScope + "OWNER_ALREADY_REGISTERED";

    public const string PetNotFound = DefaultScope + "PET_NOT_FOUND";

    public const string TagNotFound = DefaultScope + "TAG_NOT_FOUND";
    public const string TagAlreadyInUse = DefaultScope + "TAG_ALREADY_IN_USE";
    public const string TagAlreadyExists = DefaultScope + "TAG_ALREADY_EXISTS";

    public const string AdminNotFound = DefaultScope + "ADMIN_NOT_FOUND";
    public const string AdminAlreadyCreated = DefaultScope + "ADMIN_ALREADY_CREATED";
}