using MediatR;
using System;

namespace SeturAssessment.Messages.Commands
{
    public class DeleteGuide : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
