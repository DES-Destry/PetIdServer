using MediatR;
using PetIdServer.Application.AppDomain.TagReportDomain.Dto;

namespace PetIdServer.Application.AppDomain.TagReportDomain.Queries.GetAll;

public class GetAllTagReportsQuery : IRequest<TagReportsDto>
{
    public int? TagId { get; set; }
    public bool? IsResolved { get; set; }
}
