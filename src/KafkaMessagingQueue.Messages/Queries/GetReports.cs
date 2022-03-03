using MediatR;
using KafkaMessagingQueue.Messages.Models;

namespace KafkaMessagingQueue.Messages.Queries
{
    public class GetReports : IRequest<ReportModel[]> { }
}
