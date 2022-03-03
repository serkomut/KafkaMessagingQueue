using System;

namespace KafkaMessagingQueue.Messages.Models
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public Guid GuideId { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public string Value { get; set; }
    }
}
