using MassTransit;
using SeturAssessment.ReportApi.Application.Events.Models;
using SeturAssessment.ReportApi.Application.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace SeturAssessment.ReportApi.Application.Events.Consumers
{
    public class ReportByLocationCompletedConsumer : IConsumer<ReportByLocationCompleted>
    {
        private readonly ReportContext db;
        public ReportByLocationCompletedConsumer(ReportContext db)
        {
            this.db = db;
        }

        public Task Consume(ConsumeContext<ReportByLocationCompleted> context)
        {
            var message = context.Message;
            var item = db.Reports.First(x => x.Id == message.ReportId);
            item.Status = Domain.Status.Completed;
            item.Data = message.Data;
            db.Reports.Update(item);
            db.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
