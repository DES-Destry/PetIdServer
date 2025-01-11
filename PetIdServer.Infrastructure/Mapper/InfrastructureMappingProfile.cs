using AutoMapper;
using PetIdServer.Core.Pets;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Users;
using PetIdServer.Infrastructure.Database.Entities;

namespace PetIdServer.Infrastructure.Mapper;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<UserEntity, User>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (UserId)model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                       expression => expression.MapFrom(domain => domain.Id));

        CreateMap<UserContactEntity, UserContact>().ReverseMap();
        CreateMap<PetEntity, Pet>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (PetId)model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                       expression => expression.MapFrom(domain => domain.Id));

        CreateMap<TagEntity, Tag>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (TagId)model.Id))
            .ForMember(entity => entity.PrivateCode, expression => expression.MapFrom(model => model.Code))
            .ReverseMap()
            .ForMember(model => model.Id,
                       expression => expression.MapFrom(domain => domain.Id))
            .ForMember(model => model.Code, expression => expression.MapFrom(entity => entity.PrivateCode));

        CreateMap<TagReportEntity, TagReport>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (TagReportId)model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                       expression => expression.MapFrom(domain => domain.Id))
            .ForMember(model => model.ReporterId,
                       expression => expression.MapFrom(domain => (Guid)domain.Reporter.Id))
            .ForMember(model => model.ResolverId,
                       expression => expression.MapFrom(domain => (Guid)domain!.Resolver!.Id));
    }
}
