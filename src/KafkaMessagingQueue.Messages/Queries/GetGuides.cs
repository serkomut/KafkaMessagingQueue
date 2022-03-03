using MediatR;
using KafkaMessagingQueue.Messages.Models;

namespace KafkaMessagingQueue.Messages.Queries
{
    public class GetGuides : IRequest<QueryableResponse<GuideModel>>
    {
        public string Filter { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public GetGuides()
        {
            Skip = 0;
            Take = 10;
        }
    }
}
