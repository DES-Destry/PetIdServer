using AutoMapper;
using PetIdServer.Application.TagReports.Dto;
using PetIdServer.Core.TagReports;

namespace PetIdServer.Application.TagReports;

public class TagReportMappingProfile : Profile
{
    public TagReportMappingProfile()
    {
        CreateMap<TagReport, TagReportShortDto>();
    }
}
