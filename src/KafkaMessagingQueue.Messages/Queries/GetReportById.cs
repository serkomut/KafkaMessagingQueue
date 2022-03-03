using MediatR;
using KafkaMessagingQueue.Messages.Models;
using System;

namespace KafkaMessagingQueue.Messages.Queries
{
    public class GetReportById : IRequest<ReportModel>
    {
        public Guid Id { get; set; }
    }
}
