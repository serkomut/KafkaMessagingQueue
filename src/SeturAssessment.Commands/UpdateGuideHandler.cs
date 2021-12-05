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

    public class UpdateGuideHandler : IRequestHandler<UpdateGuide, CommandResponse<Guid>>
    {
        private readonly SeturContext context;
        public UpdateGuideHandler(SeturContext context)
        {
            this.context = context;
        }

        public async Task<CommandResponse<Guid>> Handle(UpdateGuide request, CancellationToken cancellationToken)
        {
            var item = await context.Guides.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                throw new Exception("Kayıt bulunamadı!");

            item.Name = request.Name;
            item.Surname = request.Surname;
            item.Company = request.Company;
            item.UpdateBy = request.UpdateBy;
            item.UpdateDate = DateTime.Now;

            context.Guides.Update(item);
            await context.SaveChangesAsync(cancellationToken);
            var response = new CommandResponse<Guid>
            {
                Aggregate = item.Id,
            };
            return response;
        }
    }
}
