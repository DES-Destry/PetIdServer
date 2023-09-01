using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace PetIdServer.RestApi.Response.Error;

public class ServerProblemDetailsFactory : ProblemDetailsFactory
{
     private readonly ApiBehaviorOptions _options;
    private Exception? _exception;

    public ServerProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override ServerProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        _exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var code = GetCodeFromException();
        statusCode = GetStatusCodeFromException();
        detail ??= GetDetailsFromException();
        title ??= ServerProblemDetailsDefaults.DefaultTitle;
        instance ??= _exception?.GetType().ToString();

#if DEBUG
        var stackTrace = _exception?.StackTrace;
#else
        const string stackTrace = "Hidden";
#endif

        var problemDetails = new ServerProblemDetails()
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
            Code = code,
            StackTrace = stackTrace,
            // Metadata = (_exception as CoreException)?.Metadata,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= ServerProblemDetailsDefaults.DefaultValidationErrorStatusCode;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }
    }

    private string GetCodeFromException()
    {
        // if (_exception is not ScopedException scopedException) return ServerProblemDetailsDefaults.DefaultErrorCode;
        
        // scopedException.ApplyScope("HTTP");
        // return scopedException.Code;
        return ServerProblemDetailsDefaults.DefaultErrorCode;
    }
    
    private int GetStatusCodeFromException()
    {
        // if (_exception is CoreException coreException)
        //     return ServerProblemDetailsDefaults.HttpErrorCodesByErrorKind[
        //         coreException.Kind ?? CoreExceptionKind.Default];
        //
        // if (_exception is IntegrationException)
        //     return ServerProblemDetailsDefaults.DefaultIntegrationErrorStatusCode;

        return ServerProblemDetailsDefaults.DefaultServerErrorStatusCode;
    }

    private string GetDetailsFromException()
    {
#if DEBUG
        return _exception?.Message ?? ServerProblemDetailsDefaults.DefaultHiddenErrorMessage;
#else
        return ServerProblemDetailsDefaults.DefaultHiddenErrorMessage;
#endif
    }
}