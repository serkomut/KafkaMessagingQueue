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
    public class AddContactHandler : IRequestHandler<AddContact, CommandResponse<Guid>>
    {
        private readonly DatabaseContext context;
        public AddContactHandler(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<CommandResponse<Guid>> Handle(AddContact request, CancellationToken cancellationToken)
        {
            var exists = await context.Contacts.AnyAsync(x => x.Value == request.Value && x.ContactType != ContactType.LOCATION, cancellationToken);
            if (exists)
                throw new Exception("Bu kayıt daha önce eklenmiş!");
            
            var contact = new Contact
            {
                GuideId = request.GuideId,
                Value = request.Value,
                ContactType = (ContactType)request.ContactType,
                CreateBy = request.CreateBy
            };

            await context.Contacts.AddAsync(contact, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            var response = new CommandResponse<Guid>
            {
                Aggregate = contact.Id,
            };
            return response;
        }
    }
}
