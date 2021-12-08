using MediatR;
using SeturAssessment.Messages.Models;
using System;

namespace SeturAssessment.Messages.Queries
{
    public record GetContactById(Guid Id) : IRequest<ContactDto>;
}
