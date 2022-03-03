namespace KafkaMessagingQueue.Messages.Models
{
    public class GuideModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactDto[] Contacts { get; set; }
    }
}
