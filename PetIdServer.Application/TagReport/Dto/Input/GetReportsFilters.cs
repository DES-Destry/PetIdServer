namespace PetIdServer.Application.TagReport.Dto.Input;

public record GetReportsFilters(int? TagId = null, bool? IsResolved = null);
