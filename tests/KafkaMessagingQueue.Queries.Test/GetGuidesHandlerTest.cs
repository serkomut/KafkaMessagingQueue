using KafkaMessagingQueue.Persistence;
using KafkaMessagingQueue.Queries.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using KafkaMessagingQueue.Messages.Queries;
using System.Threading;

namespace KafkaMessagingQueue.Queries.Test
{
    public class GetGuidesHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly DatabaseContext context;
        private readonly GetGuidesHandler handler;
        public GetGuidesHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<DatabaseContext>();
            handler = new GetGuidesHandler(context);
        }

        [Theory]
        [InlineData(0, 5, "")]
        [InlineData(0, 5, "Test")]
        public async Task Handle_Should_Be_Success_When_Skip_Take(int skip, int take, string filter)
        {
            var request = new GetGuides
            {
                Skip = skip,
                Take = take,
                Filter = filter
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Data.Length > 0);
        }
    }
}
