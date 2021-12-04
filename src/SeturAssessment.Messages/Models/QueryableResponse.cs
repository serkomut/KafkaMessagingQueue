namespace SeturAssessment.Messages.Models
{
    public class QueryableResponse<T>
    {
        public T[] Data { get; set; }
        public int TotalCount { get; set; }
        public QueryableResponse()
        {
            TotalCount = -1;
        }
    }
}
