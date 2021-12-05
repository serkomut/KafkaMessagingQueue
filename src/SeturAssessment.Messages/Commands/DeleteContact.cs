using MediatR;
using SeturAssessment.Messages.Models;
using System;

namespace SeturAssessment.Messages.Commands
{
    public record DeleteContact(Guid Id) : IRequest<CommandResponse<Guid>>;
}
