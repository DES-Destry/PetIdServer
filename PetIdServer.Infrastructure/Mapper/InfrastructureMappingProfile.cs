using AutoMapper;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;
using PetIdServer.Core.Domain.User;
using PetIdServer.Infrastructure.Database.Entities;

namespace PetIdServer.Infrastructure.Mapper;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<UserModel, UserEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (UserId)model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                       expression => expression.MapFrom(domain => domain.Id));

        CreateMap<UserContactModel, UserContact>().ReverseMap();
        CreateMap<PetModel, PetEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (PetId)model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                       expression => expression.MapFrom(domain => domain.Id));

        CreateMap<TagModel, TagEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (TagId)model.Id))
            .ForMember(entity => entity.PrivateCode, expression => expression.MapFrom(model => model.Code))
            .ReverseMap()
            .ForMember(model => model.Id,
                       expression => expression.MapFrom(domain => domain.Id))
            .ForMember(model => model.Code, expression => expression.MapFrom(entity => entity.PrivateCode));

        CreateMap<TagReportModel, TagReportEntity>()
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
