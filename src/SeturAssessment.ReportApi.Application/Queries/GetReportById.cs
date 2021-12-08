using MediatR;
using SeturAssessment.ReportApi.Application.Models;
using System;

namespace SeturAssessment.ReportApi.Application.Queries
{
    public class GetReportById : IRequest<ReportModel>
    {
        public Guid Id { get; set; }
    }
}
