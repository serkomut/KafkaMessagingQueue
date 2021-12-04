using MediatR;
using SeturAssessment.Messages.Models;
using System;

namespace SeturAssessment.Messages.Queries
{
    public record GetGuideById(Guid Id) : IRequest<GuideModel>;
}
