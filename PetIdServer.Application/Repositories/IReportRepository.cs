using PetIdServer.Core.Domain.Report;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<Report>> GetAllReports();
    Task<IEnumerable<Report>> GetNotResolvedReports();
    Task<IEnumerable<Report>> GetReportsByTagId(TagId tagId);

    Task CreateReport(Report report);
    Task UpdateReport(Guid id, Report updated);
}
