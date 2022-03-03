using MediatR;
using KafkaMessagingQueue.ReportApi.Application.Models;
using System;

namespace KafkaMessagingQueue.ReportApi.Application.Queries
{
    public class GetReportById : IRequest<ReportModel>
    {
        public Guid Id { get; set; }
    }
}
