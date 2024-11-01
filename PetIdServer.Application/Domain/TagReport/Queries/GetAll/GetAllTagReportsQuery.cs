using MediatR;
using PetIdServer.Application.Domain.TagReport.Dto;

namespace PetIdServer.Application.Domain.TagReport.Queries.GetAll;

public class GetAllTagReportsQuery : IRequest<TagReportsDto>
{
    public int? TagId { get; set; }
    public bool? IsResolved { get; set; }
}
