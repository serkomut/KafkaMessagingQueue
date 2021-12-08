using System;

namespace SeturAssessment.ReportApi.Application.Events.Models
{
    public class ReportByLocationPreparing
    {
        public Guid ReportId { get; set; }
        public string Name { get; set; }
        public string CreateBy { get; set; }
        public string Data { get; set; }
    }
}
