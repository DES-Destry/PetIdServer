namespace PetIdServer.Application.AppDomain.TagReportDomain.Dto.Input;

public record GetReportsFilters(int? TagId = null, bool? IsResolved = null);
