using MediatR;

namespace SeturAssessment.Messages.Events
{
    public class ReportEventByLocation : INotification
    {
        public string Location { get; set; }
    }
}
