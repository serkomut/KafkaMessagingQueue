using SeturAssessment.Persistence;
using SeturAssessment.Queries.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Messages.Queries;
using System.Threading;

namespace SeturAssessment.Queries.Test
{
    public class GetContacsByGuideIdHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        private readonly GetContacsByGuideIdHandler handler;
        public GetContacsByGuideIdHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new GetContacsByGuideIdHandler(context);
        }

        [Fact]
        public async Task Handle_Should_Be_Success()
        {
            var guide = await context.Guides.FirstAsync();
            var request = new GetContacsByGuideId(guide.Id);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }
    }
}
