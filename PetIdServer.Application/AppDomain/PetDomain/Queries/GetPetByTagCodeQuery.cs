using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;

namespace PetIdServer.Application.AppDomain.PetDomain.Queries;

public class GetPetByTagCodeQuery : IRequest<PetDto>
{
    public string Code { get; set; }
}
