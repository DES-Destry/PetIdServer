using PetIdServer.Application.Tags.Commands.CreateBatch;

namespace PetIdServer.RestApi.Endpoints.Dto.Admin;

public record CreateTagsDto(int IdFrom, int IdTo, IEnumerable<string> Codes, IEnumerable<string>? Features)
{
    public CreateTagsBatchCommand ToCommand() => new()
    {
        IdFrom = IdFrom, IdTo = IdTo, Codes = Codes, Features = Features
    };
}
