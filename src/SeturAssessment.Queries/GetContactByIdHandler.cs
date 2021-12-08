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
    public class GetContactByIdHandler : IRequestHandler<GetContactById, ContactDto>
    {
        private readonly SeturContext context;

        public GetContactByIdHandler(SeturContext context)
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
