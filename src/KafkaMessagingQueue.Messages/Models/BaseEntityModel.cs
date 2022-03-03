using System;

namespace KafkaMessagingQueue.Messages.Models
{
    public class BaseEntityModel
    {
        public Guid Id  { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
