using MediatR;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Messages.Models;
using SeturAssessment.Messages.Queries;
using SeturAssessment.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SeturAssessment.Queries
{

    public class GetGuidesHandler : IRequestHandler<GetGuides, QueryableResponse<GuideModel>>
    {
        private readonly SeturContext context;
        public GetGuidesHandler(SeturContext context)
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
