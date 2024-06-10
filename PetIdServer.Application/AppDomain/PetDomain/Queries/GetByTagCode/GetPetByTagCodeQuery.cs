using MediatR;
using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagCode;

public class GetPetByTagCodeQuery : IRequest<PetDto>
{
    public string Code { get; set; }
}
