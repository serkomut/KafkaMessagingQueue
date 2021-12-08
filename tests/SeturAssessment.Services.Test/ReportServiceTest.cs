using Newtonsoft.Json;
using SeturAssessment.Services.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SeturAssessment.Services.Test
{
    public class ReportServiceTest
    {
        Uri uri;
        public ReportServiceTest()
        {
            uri = new Uri("http://test.com");
        }
        [Fact]
        public async Task Service_Should_Return_Report_When_ACorrect_Url_Is_Provided()
        {
            var report = new ReportModel
            {
                Id = Guid.NewGuid(),
                CreateBy = "SYSTEM",
                CreateDate = DateTime.Now,
                Data = "{}",
                Name = "Name"
            };
            
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(report), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler)
            {
                BaseAddress = uri
            };

            // Act
            var service = new ReportService(fakeHttpClient);
            var result = await service.GetReportById(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(report.Id, result.Id);
            Assert.Equal(report.CreateDate, result.CreateDate);
            Assert.Equal(report.Data, result.Data);
            Assert.Equal(report.Name, result.Name);
            Assert.Equal(report.CreateBy, result.CreateBy);
        }
    }
}
