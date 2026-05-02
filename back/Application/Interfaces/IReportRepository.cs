using Domain;
using Domain.Entities.Report;

namespace Application.Interfaces;

public interface IReportRepository : IGenericRepository<ReportEntity, Guid>
{
    Task<bool> ExistsAsync(Guid senderId, Guid contentId, ContentTypes contentType);
}