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
    public class DeleteContactHandler : IRequestHandler<DeleteContact, CommandResponse<Guid>>
    {
        private readonly SeturContext context;
        public DeleteContactHandler(SeturContext context)
        {
            this.context = context;
        }

        public async Task<CommandResponse<Guid>> Handle(DeleteContact request, CancellationToken cancellationToken)
        {
            var item = await context.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                throw new Exception("Kayıt bulunamadı!");
            else
            {
                context.Contacts.Remove(item);
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
