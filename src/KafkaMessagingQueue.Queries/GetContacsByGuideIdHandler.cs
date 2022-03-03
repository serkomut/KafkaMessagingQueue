using MediatR;
using Microsoft.EntityFrameworkCore;
using KafkaMessagingQueue.Messages.Models;
using KafkaMessagingQueue.Messages.Queries;
using KafkaMessagingQueue.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.Queries
{
    public class GetContacsByGuideIdHandler : IRequestHandler<GetContacsByGuideId, ContactDto[]>
    {
        private readonly DatabaseContext context;

        public GetContacsByGuideIdHandler(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<ContactDto[]> Handle(GetContacsByGuideId request, CancellationToken cancellationToken)
        {
            var contacts = await context.Contacts.Where(x => x.GuideId == request.GuideId)
                .Select(x => x.ContactMap()).ToArrayAsync();
            return contacts;
        }
    }
}
