using KafkaMessagingQueue.ReportApi.Application.Persistence;
using KafkaMessagingQueue.ReportApi.Application.Queries;
using KafkaMessagingQueue.ReportApi.Application.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace KafkaMessagingQueue.ReportApi.Application.Test
{
    public class GetReportByIdHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly ReportContext context;
        private readonly GetReportByIdHandler handler;
        public GetReportByIdHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<ReportContext>();
            handler = new GetReportByIdHandler(context);
        }

        [Fact]
        public async Task Handle_Should_Be_Success()
        {
            var guide = await context.Reports.FirstAsync();
            var request = new GetReportById
            {
                Id = guide.Id
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(guide.Name, result.Name);
            Assert.Equal(guide.Data, result.Data);
        }
    }
}
