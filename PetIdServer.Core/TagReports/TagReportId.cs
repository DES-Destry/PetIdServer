using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.TagReports;

public record TagReportId(Guid Value)
{
    [return: NotNullIfNotNull("tagReportId")]
    public static implicit operator Guid?(TagReportId? tagReportId) => tagReportId?.Value;

    public static implicit operator Guid(TagReportId tagReportId) => tagReportId.Value;


    [return: NotNullIfNotNull("id")]
    public static explicit operator TagReportId?(Guid? id) => id is null ? null : new TagReportId(id.Value);

    public static explicit operator TagReportId(Guid id) => new(id);
}
