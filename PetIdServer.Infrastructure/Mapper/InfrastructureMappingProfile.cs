using AutoMapper;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;
using PetIdServer.Infrastructure.Database.Models;

namespace PetIdServer.Infrastructure.Mapper;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<OwnerModel, OwnerEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (OwnerId) model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                expression => expression.MapFrom(domain => domain.Id));

        CreateMap<OwnerContactModel, OwnerContactVo>().ReverseMap();
        CreateMap<PetModel, PetEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (PetId) model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                expression => expression.MapFrom(domain => domain.Id));

        CreateMap<TagModel, TagEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (TagId) model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                expression => expression.MapFrom(domain => domain.Id));

        CreateMap<TagReportModel, TagReportEntity>()
            .ForCtorParam("id", expression => expression.MapFrom(model => (TagReportId) model.Id))
            .ReverseMap()
            .ForMember(model => model.Id,
                expression => expression.MapFrom(domain => domain.Id))
            .ForMember(model => model.ReporterId,
                expression => expression.MapFrom(domain => (string) domain.Reporter.Id))
            .ForMember(model => model.ResolverId,
                expression => expression.MapFrom(domain => (string) domain!.Resolver!.Id));

        CreateMap<AdminModel, AdminEntity>()
            .ForCtorParam("id",
                expression => expression.MapFrom(model => (AdminId) model.Username))
            .ReverseMap()
            .ForMember(model => model.Username,
                expression => expression.MapFrom(domain => domain.Id));
    }
}
