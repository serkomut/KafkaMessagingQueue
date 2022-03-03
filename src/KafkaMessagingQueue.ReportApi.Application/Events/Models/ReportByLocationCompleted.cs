using System;

namespace KafkaMessagingQueue.ReportApi.Application.Events.Models
{
    public class ReportByLocationCompleted
    {
        public Guid ReportId { get; set; }
        public string Name { get; set; }
        public string CreateBy { get; set; }
        public string Data { get; set; }
    }
}
