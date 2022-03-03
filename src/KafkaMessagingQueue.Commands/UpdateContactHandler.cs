using MediatR;
using Microsoft.EntityFrameworkCore;
using KafkaMessagingQueue.Domain;
using KafkaMessagingQueue.Messages.Commands;
using KafkaMessagingQueue.Messages.Models;
using KafkaMessagingQueue.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.Commands
{
    public class UpdateContactHandler : IRequestHandler<UpdateContact, CommandResponse<Guid>>
    {
        private readonly DatabaseContext context;
        public UpdateContactHandler(DatabaseContext context)
        {
            this.context = context;
        }
        
        public async Task<CommandResponse<Guid>> Handle(UpdateContact request, CancellationToken cancellationToken)
        {
            var item = await context.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                throw new Exception("Kayıt bulunamadı!");

            var exists = await context.Contacts.AnyAsync(x => x.Value == request.Value && x.Id != request.Id && x.ContactType != ContactType.LOCATION, cancellationToken);
            if (exists)
                throw new Exception("Bu kayıt daha önce eklenmiş!");

            item.ContactType = (ContactType)request.ContactType;
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
