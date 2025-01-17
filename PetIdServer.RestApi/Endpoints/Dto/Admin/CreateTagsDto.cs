using PetIdServer.Application.Tags.Commands.CreateBatch;

namespace PetIdServer.RestApi.Endpoints.Dto.Admin;

public record CreateTagsDto(int IdFrom, int IdTo, IEnumerable<string> Codes)
{
    public CreateTagsBatchCommand ToCommand() => new()
    {
        IdFrom = IdFrom, IdTo = IdTo, Codes = Codes
    };
}
