using SeturAssessment.Services.Models;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SeturAssessment.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient client;

        public ReportService(HttpClient client)
        {
            this.client = client;
        }

        public Task<ReportModel> GetReportById(Guid id, CancellationToken cancellationToken = default)
        {
            var url = $"v1/Report/{id}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            return client.SendAsync<ReportModel>(httpRequest, cancellationToken);
        }

        public Task<ReportModel[]> GetReports(CancellationToken cancellationToken = default)
        {
            var url = "v1/Report/GetReports";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            return client.SendAsync<ReportModel[]>(httpRequest, cancellationToken);
        }
    }
}
