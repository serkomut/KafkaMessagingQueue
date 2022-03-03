using MediatR;
using KafkaMessagingQueue.ReportApi.Application.Models;

namespace KafkaMessagingQueue.ReportApi.Application.Queries
{
    public class GetReports : IRequest<ReportModel[]> { }
}
