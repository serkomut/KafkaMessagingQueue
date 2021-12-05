using MediatR;
using SeturAssessment.Messages.Models;

namespace SeturAssessment.Messages.Queries
{
    public class GetGuides : IRequest<QueryableResponse<GuideModel>>
    {
        public string Filter { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public GetGuides()
        {
            Skip = 0;
            Take = 10;
        }
    }
}
