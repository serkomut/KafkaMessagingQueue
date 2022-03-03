using System;

namespace KafkaMessagingQueue.Services.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Status { get; set; }
    }
}
