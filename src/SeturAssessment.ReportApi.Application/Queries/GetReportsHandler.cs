using MediatR;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.ReportApi.Application.Models;
using SeturAssessment.ReportApi.Application.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SeturAssessment.ReportApi.Application.Queries
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
