using System;

namespace KafkaMessagingQueue.ReportApi.Application.Domain
{
    public class Report
    {
        private DateTime? createdDate;
        private Guid? id;
        public Guid Id
        {
            get => id ?? Guid.NewGuid();
            set => id = value;
        }
        public DateTime CreateDate
        {
            get => createdDate ?? DateTime.UtcNow;
            set => createdDate = value;
        }
        public string Name { get; set; }
        public string Data { get; set; }
        public Status Status { get; set; }
        public string CreateBy { get; set; }
    }

    public enum Status : byte
    {
        Preparing = 1,
        Completed = 2
    }
}
