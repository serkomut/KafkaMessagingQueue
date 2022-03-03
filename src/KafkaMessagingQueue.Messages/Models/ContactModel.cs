using System;

namespace KafkaMessagingQueue.Messages.Models
{
    public class ContactModel
    {
        public ContactTypeEnum ContactType { get; set; }
        public string Value { get; set; }
    }
}
