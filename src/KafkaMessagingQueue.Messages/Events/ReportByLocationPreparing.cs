using System;

namespace KafkaMessagingQueue.Messages.Events
{
    public class ReportByLocationPreparing
    {
        public Guid ReportId { get; set; }
        public string Name { get; set; }
        public string CreateBy { get; set; }
    }

    public class ReportByLocationCompleted : ReportByLocationPreparing
    {
        public string Data { get; set; }
    }
}
