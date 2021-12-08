using MediatR;
using SeturAssessment.ReportApi.Application.Models;

namespace SeturAssessment.ReportApi.Application.Queries
{
    public class GetReports : IRequest<ReportModel[]> { }
}
