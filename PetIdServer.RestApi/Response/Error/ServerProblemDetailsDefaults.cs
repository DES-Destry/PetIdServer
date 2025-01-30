using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.RestApi.Response.Error;

public class ServerProblemDetailsDefaults
{
    public const string DefaultErrorCode = "API.UNKNOWN_ERROR";

    public const int DefaultValidationErrorStatusCode = 400;

    public const int DefaultServerErrorStatusCode = 500;

    public const int DefaultIntegrationErrorStatusCode = 503;

    public const string DefaultTitle = "Some error occurred in PetID Server.";

    public const string DefaultHiddenErrorMessage =
        "Unknown error occurred in PetID API. Contact with developers to resolve this problem (better don't contact)";

    public static readonly Dictionary<ExceptionKind, int> HttpErrorCodesByErrorKind = new()
    {
        [ExceptionKind.Default] = 500,
        [ExceptionKind.MethodNotImplemented] = 501,
        [ExceptionKind.UserInputIsNotValid] = 400,
        [ExceptionKind.UserAuthenticationRequired] = 401,
        [ExceptionKind.NotEnoughResources] = 402,
        [ExceptionKind.UserAuthorizationRequired] = 403,
        [ExceptionKind.EntityNotFound] = 404,
        [ExceptionKind.EntitiesConflicting] = 409
    };
}
