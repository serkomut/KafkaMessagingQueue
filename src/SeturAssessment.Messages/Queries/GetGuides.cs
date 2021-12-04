using MediatR;
using SeturAssessment.Messages.Models;

namespace SeturAssessment.Messages.Queries
{
    public record GetGuides : IRequest<QueryableResponse<GuideModel>>;
}
