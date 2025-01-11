using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domain.TagReport.Exceptions;

public class TagReportNotFoundException : CoreException
{
    public TagReportNotFoundException(string message = "Tag report not found") : base(message) { }

    public TagReportNotFoundException(object metadata) : base(metadata) { }

    public TagReportNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagNotFound;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntityNotFound;
}
