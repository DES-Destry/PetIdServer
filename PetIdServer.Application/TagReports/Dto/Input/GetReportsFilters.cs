namespace PetIdServer.Application.TagReports.Dto.Input;

public record GetReportsFilters(int? TagId = null, bool? IsResolved = null);
