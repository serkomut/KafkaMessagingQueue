using MediatR;
using SeturAssessment.Messages.Models;

namespace SeturAssessment.Messages.Queries
{
    public class GetReports : IRequest<ReportModel[]> { }
}
