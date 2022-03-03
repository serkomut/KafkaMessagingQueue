using MediatR;
using Newtonsoft.Json;
using KafkaMessagingQueue.Messages.Models;
using KafkaMessagingQueue.Messages.Queries;
using KafkaMessagingQueue.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.Queries
{
    public class GetReportsHandler : IRequestHandler<GetReports, ReportModel[]>
    {
        IReportService reportService;

        public GetReportsHandler(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public async Task<ReportModel[]> Handle(GetReports request, CancellationToken cancellationToken)
        {
            var items = await reportService.GetReports();
            if (items == null)
                return null;
            var result= items.Select(x => new ReportModel
            {
                Id = x.Id,
                Name = x.Name,
                CreateBy = x.CreateBy,
                CreateDate = x.CreateDate,
                Data = !string.IsNullOrWhiteSpace(x.Data) ? JsonConvert.DeserializeObject<ReportLocationModel>(x.Data) : null,
                Status = x.Status
            }).ToArray();

            return result;
        }
    }
}
