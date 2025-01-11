namespace PetIdServer.Core.Domain.TagReport;

public record TagReportId(Guid Value)
{
    public static implicit operator Guid(TagReportId tagReportId) => tagReportId.Value;
    public static explicit operator TagReportId(Guid id) => new(id);
}
