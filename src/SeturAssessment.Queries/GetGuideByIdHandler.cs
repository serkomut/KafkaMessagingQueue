using MediatR;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Messages.Models;
using SeturAssessment.Messages.Queries;
using SeturAssessment.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeturAssessment.Queries
{
    public class GetGuideByIdHandler : IRequestHandler<GetGuideById, GuideModel>
    {
        private readonly SeturContext context;

        public GetGuideByIdHandler(SeturContext context)
        {
            this.context = context;
        }

        public async Task<GuideModel> Handle(GetGuideById request, CancellationToken cancellationToken)
        {
            var guide = await context.Guides
                .Include(x => x.Contacts)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (guide == null)
                throw new Exception("Kayıt bulunamadı!");

            return guide.GuideMap();
        }
    }
}
