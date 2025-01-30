using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.TagReports.Exceptions;

public sealed class TagReportNotFoundException : CoreException
{
    private const string DefaultMessage = "Tag report not found";

    public TagReportNotFoundException(string message = DefaultMessage) : base(message) { }
    public TagReportNotFoundException(object metadata) : base(metadata) { }
    public TagReportNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagNotFound;
    public override ExceptionKind? Kind => ExceptionKind.EntityNotFound;
}
