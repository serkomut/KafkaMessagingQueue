using MediatR;

namespace KafkaMessagingQueue.Messages.Events
{
    public class ReportEventByLocation : INotification
    {
        public string Location { get; set; }
    }
}
