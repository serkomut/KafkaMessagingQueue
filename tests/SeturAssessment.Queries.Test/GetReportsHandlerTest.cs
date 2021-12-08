using SeturAssessment.Queries.Test.Infrastructure;
using Xunit;
using System.Threading.Tasks;
using SeturAssessment.Messages.Queries;
using System.Threading;
using Moq;
using SeturAssessment.Services;

namespace SeturAssessment.Queries.Test
{
    public class GetReportsHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly GetReportsHandler handler;
        IMock<IReportService> mockService;
        public GetReportsHandlerTest(DataModuleFixture dataModuleFixture)
        {
            mockService = new Mock<IReportService>();
            handler = new GetReportsHandler(mockService.Object);
        }

        [Fact]
        public async Task Handle_Should_Be_Success()
        {
            var request = new GetReports();
            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.Length == 0);
        }
    }
}
