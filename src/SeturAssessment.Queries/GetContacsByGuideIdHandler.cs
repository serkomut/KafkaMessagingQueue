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
    public class GetContacsByGuideIdHandler : IRequestHandler<GetContacsByGuideId, ContactDto[]>
    {
        private readonly SeturContext context;

        public GetContacsByGuideIdHandler(SeturContext context)
        {
            this.context = context;
        }

        public async Task<ContactDto[]> Handle(GetContacsByGuideId request, CancellationToken cancellationToken)
        {
            var contacts = await context.Contacts.Where(x => x.GuideId == request.GuidId)
                .Select(x => x.ContactMap()).ToArrayAsync();
            return contacts;
        }
    }
}
