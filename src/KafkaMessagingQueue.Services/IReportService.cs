using KafkaMessagingQueue.Services.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.Services
{
    public interface IReportService
    {
        Task<ReportModel> GetReportById(Guid id, CancellationToken cancellationToken = default);
        Task<ReportModel[]> GetReports(CancellationToken cancellationToken = default);
    }
}
