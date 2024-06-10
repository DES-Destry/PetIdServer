using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;

namespace PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagId;

public class GetPetByTagIdQuery : IRequest<PetDto>
{
    public int TagId { get; set; }
}
