using MediatR;
using PetIdServer.Application.TagReport.Dto;

namespace PetIdServer.Application.TagReport.Queries.GetAll;

public class GetAllTagReportsQuery : IRequest<TagReportsDto>
{
    public int? TagId { get; set; }
    public bool? IsResolved { get; set; }
}
