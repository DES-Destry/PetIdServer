using MediatR;
using PetIdServer.Application.TagReports.Dto;

namespace PetIdServer.Application.TagReports.Queries.GetAll;

public class GetAllTagReportsQuery : IRequest<TagReportsDto>
{
    public int? TagId { get; init; }
    public bool? IsResolved { get; init; }
}
