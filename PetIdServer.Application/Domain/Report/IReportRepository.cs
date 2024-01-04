using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Domain.Report;

public interface IReportRepository
{
    Task<IEnumerable<Core.Domain.Report.Report>> GetAllReports();
    Task<IEnumerable<Core.Domain.Report.Report>> GetNotResolvedReports();
    Task<IEnumerable<Core.Domain.Report.Report>> GetReportsByTagId(TagId tagId);

    Task CreateReport(Core.Domain.Report.Report report);
    Task UpdateReport(Guid id, Core.Domain.Report.Report updated);
}
