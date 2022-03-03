using MediatR;
using Microsoft.EntityFrameworkCore;
using KafkaMessagingQueue.Messages.Models;
using KafkaMessagingQueue.Messages.Queries;
using KafkaMessagingQueue.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.Queries
{
    public class GetGuideByIdHandler : IRequestHandler<GetGuideById, GuideModel>
    {
        private readonly DatabaseContext context;

        public GetGuideByIdHandler(DatabaseContext context)
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
