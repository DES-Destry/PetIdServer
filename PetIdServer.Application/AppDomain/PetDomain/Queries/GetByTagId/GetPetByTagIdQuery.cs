using MediatR;
using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagId;

public class GetPetByTagIdQuery : IRequest<PetDto>
{
    public int TagId { get; set; }
}
