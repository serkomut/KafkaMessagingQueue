using System;

namespace KafkaMessagingQueue.Messages.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ReportLocationModel Data { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Status { get; set; }
    }
}
