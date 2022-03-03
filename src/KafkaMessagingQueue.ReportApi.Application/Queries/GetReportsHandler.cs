using MediatR;
using Microsoft.EntityFrameworkCore;
using KafkaMessagingQueue.ReportApi.Application.Models;
using KafkaMessagingQueue.ReportApi.Application.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.ReportApi.Application.Queries
{
    public class GetReportsHandler : IRequestHandler<GetReports, ReportModel[]>
    {
        private readonly ReportContext context;

        public GetReportsHandler(ReportContext context)
        {
            this.context = context;
        }

        public Task<ReportModel[]> Handle(GetReports request, CancellationToken cancellationToken)
        {
            var reports = context.Reports.Select(x => ReportModel.Map(x)).ToArrayAsync();
            return reports;
        }
    }
}
