using MassTransit;
using SeturAssessment.ReportApi.Application.Events.Models;
using SeturAssessment.ReportApi.Application.Persistence;
using System.Threading.Tasks;

namespace SeturAssessment.ReportApi.Application.Events.Consumers
{

    public class ReportByLocationPreparingConsumer : IConsumer<ReportByLocationPreparing>
    {
        private readonly ReportContext db;
        public ReportByLocationPreparingConsumer(ReportContext db)
        {
            this.db = db;
        }

        public Task Consume(ConsumeContext<ReportByLocationPreparing> context)
        {
            var message = context.Message;
            var model = new Domain.Report
            {
                Id = message.ReportId,
                CreateBy = message.CreateBy,
                Data = message.Data,
                Name = message.Name,
                Status = Domain.Status.Preparing
            };
            db.Reports.Add(model);
            db.SaveChanges();
            db.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
