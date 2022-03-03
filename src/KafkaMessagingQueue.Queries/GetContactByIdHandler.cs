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
    public class GetContactByIdHandler : IRequestHandler<GetContactById, ContactDto>
    {
        private readonly DatabaseContext context;

        public GetContactByIdHandler(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<ContactDto> Handle(GetContactById request, CancellationToken cancellationToken)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (contact == null)
                throw new Exception("Kayıt bulunamadı!");

            return contact.ContactMap();
        }
    }
}
