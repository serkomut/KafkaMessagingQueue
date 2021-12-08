using MediatR;
using SeturAssessment.Messages.Models;
using System;

namespace SeturAssessment.Messages.Queries
{
    public record GetContacsByGuideId(Guid GuideId) : IRequest<ContactDto[]>;
}
