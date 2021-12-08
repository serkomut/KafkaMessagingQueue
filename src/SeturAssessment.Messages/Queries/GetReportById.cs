using MediatR;
using SeturAssessment.Messages.Models;
using System;

namespace SeturAssessment.Messages.Queries
{
    public class GetReportById : IRequest<ReportModel>
    {
        public Guid Id { get; set; }
    }
}
