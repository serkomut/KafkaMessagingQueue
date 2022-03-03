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

    public class GetGuidesHandler : IRequestHandler<GetGuides, QueryableResponse<GuideModel>>
    {
        private readonly DatabaseContext context;
        public GetGuidesHandler(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<QueryableResponse<GuideModel>> Handle(GetGuides request, CancellationToken cancellationToken)
        {
            var query = context.Guides
                .Include(x => x.Contacts)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter))
                query = query.Where(x => x.Name.Contains(request.Filter) || x.Surname.Contains(request.Filter));

            var count = await query.CountAsync();
            query = query.Skip(request.Skip)
                .Take(request.Take);

            var response = new QueryableResponse<GuideModel>
            {
                TotalCount = count,
                Data = query.Select(x => x.GuideMap()).ToArray()
            };
            return response;
        }

        
    }
}
