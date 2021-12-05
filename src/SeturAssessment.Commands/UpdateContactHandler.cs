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
    public class UpdateContactHandler : IRequestHandler<UpdateContact, CommandResponse<Guid>>
    {
        private readonly SeturContext context;
        public UpdateContactHandler(SeturContext context)
        {
            this.context = context;
        }
        
        public async Task<CommandResponse<Guid>> Handle(UpdateContact request, CancellationToken cancellationToken)
        {
            var item = await context.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                throw new Exception("Kayıt bulunamadı!");

            item.ContactType = (Domain.ContactType)request.ContactType;
            item.Value = request.Value;
            item.UpdateBy = request.UpdateBy;
            item.UpdateDate = DateTime.Now;

            context.Contacts.Update(item);
            await context.SaveChangesAsync(cancellationToken);
            var response = new CommandResponse<Guid>
            {
                Aggregate = item.Id,
            };
            return response;
        }
    }
}
