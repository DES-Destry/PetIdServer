using AutoMapper;
using PetIdServer.Application.TagReport.Dto;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.TagReport;

public class TagReportMappingProfile : Profile
{
    public TagReportMappingProfile()
    {
        CreateMap<TagReportEntity, TagReportShortDto>();
    }
}
