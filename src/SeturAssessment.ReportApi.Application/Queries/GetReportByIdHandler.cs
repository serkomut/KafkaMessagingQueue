using MediatR;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.ReportApi.Application.Models;
using SeturAssessment.ReportApi.Application.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeturAssessment.ReportApi.Application.Queries
{
    public class GetReportByIdHandler : IRequestHandler<GetReportById, ReportModel>
    {
        private readonly ReportContext context;
        public GetReportByIdHandler(ReportContext context)
        {
            this.context = context;
        }
        public async Task<ReportModel> Handle(GetReportById request, CancellationToken cancellationToken)
        {
            var item = await context.Reports.FirstOrDefaultAsync(x=> x.Id == request.Id, cancellationToken);
            if (item == null)
                throw new Exception("Kayıt bulunamadı!");

            return ReportModel.Map(item);
        }
    }
}
