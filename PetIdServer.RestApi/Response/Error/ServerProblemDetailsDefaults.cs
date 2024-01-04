using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.RestApi.Response.Error;

public class ServerProblemDetailsDefaults
{
    public const string DefaultErrorCode = "API.UNKNOWN_ERROR";

    public const int DefaultValidationErrorStatusCode = 400;

    public const int DefaultServerErrorStatusCode = 500;

    public const int DefaultIntegrationErrorStatusCode = 503;

    public const string DefaultTitle = "Some error occurred in Pet ID Server.";

    public const string DefaultHiddenErrorMessage =
        "Unknown error occurred in Quantum War API. Contact with developers to resolve this problem (better don't contact)";

    public static readonly Dictionary<CoreExceptionKind, int> HttpErrorCodesByErrorKind = new()
    {
        [CoreExceptionKind.Default] = 500,
        [CoreExceptionKind.MethodNotImplemented] = 501,

        [CoreExceptionKind.UserInputIsNotValid] = 400,
        [CoreExceptionKind.UserAuthenticationRequired] = 401,
        [CoreExceptionKind.NotEnoughResources] = 402,
        [CoreExceptionKind.UserAuthorizationRequired] = 403,
        [CoreExceptionKind.EntityNotFound] = 404,
        [CoreExceptionKind.EntitiesConflicting] = 409
    };
}