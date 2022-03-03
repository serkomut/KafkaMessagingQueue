using MediatR;
using Microsoft.EntityFrameworkCore;
using KafkaMessagingQueue.ReportApi.Application.Models;
using KafkaMessagingQueue.ReportApi.Application.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.ReportApi.Application.Queries
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
