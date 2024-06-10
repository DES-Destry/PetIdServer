using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;

namespace PetIdServer.Application.AppDomain.PetDomain.Queries;

public class GetPetByCodeQuery : IRequest<PetDto>
{
    public string PublicCode { get; set; }
}
