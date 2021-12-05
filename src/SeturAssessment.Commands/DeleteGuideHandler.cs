using MediatR;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Messages.Commands;
using SeturAssessment.Messages.Models;
using SeturAssessment.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeturAssessment.Commands
{

    public class DeleteGuideHandler : IRequestHandler<DeleteGuide, CommandResponse<Guid>>
    {
        private readonly SeturContext context;

        public DeleteGuideHandler(SeturContext context)
        {
            this.context = context;
        }

        public async Task<CommandResponse<Guid>> Handle(DeleteGuide request, CancellationToken cancellationToken)
        {
            var item = await context.Guides.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                throw new Exception("Kayıt bulunamadı!");
            else
            {
                context.Guides.Remove(item);
                await context.SaveChangesAsync(cancellationToken);
            }
            var response = new CommandResponse<Guid>
            {
                Aggregate = item.Id,
            };
            return response;
        }
    }
}
