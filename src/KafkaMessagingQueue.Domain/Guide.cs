using System.Collections.Generic;

namespace KafkaMessagingQueue.Domain
{
    public class Guide : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
