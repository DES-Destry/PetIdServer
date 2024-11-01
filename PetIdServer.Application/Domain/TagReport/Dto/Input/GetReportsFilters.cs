namespace PetIdServer.Application.Domain.TagReport.Dto.Input;

public record GetReportsFilters(int? TagId = null, bool? IsResolved = null);
