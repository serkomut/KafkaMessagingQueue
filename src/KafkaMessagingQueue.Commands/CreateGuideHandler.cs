using MediatR;
using Microsoft.EntityFrameworkCore;
using KafkaMessagingQueue.Domain;
using KafkaMessagingQueue.Messages.Commands;
using KafkaMessagingQueue.Messages.Models;
using KafkaMessagingQueue.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.Commands
{
    public class CreateGuideHandler : IRequestHandler<CreateGuide, CommandResponse<Guid>>
    {
        private readonly DatabaseContext context;
        public CreateGuideHandler(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<CommandResponse<Guid>> Handle(CreateGuide request, CancellationToken cancellationToken)
        {
            var model = new Guide
            {
                Name = request.Name,
                Surname = request.Surname,
                Company = request.Company,
                CreateBy = request.CreateBy
            };
           
            if (request.Contacts != null && request.Contacts.Any())
            {
                var contacts = new List<Contact>();
                foreach (var item in request.Contacts)
                {
                    var exists = await context.Contacts.AnyAsync(x => x.Value == item.Value && x.ContactType != ContactType.LOCATION, cancellationToken);
                    if (!exists)
                    {
                        var contact = new Contact
                        {
                            GuideId = model.Id,
                            Value = item.Value,
                            ContactType = (ContactType)item.ContactType,
                            CreateBy = request.CreateBy
                        };
                        contacts.Add(contact);
                    }
                }
                model.Contacts = contacts;
            }

            await context.Guides.AddAsync(model, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            var response = new CommandResponse<Guid>
            {
                Aggregate = model.Id,
            };
            return response;
        }
    }
}
