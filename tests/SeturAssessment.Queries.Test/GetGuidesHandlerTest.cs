using SeturAssessment.Persistence;
using SeturAssessment.Queries.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using SeturAssessment.Messages.Queries;
using System.Threading;

namespace SeturAssessment.Queries.Test
{
    public class GetGuidesHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        private readonly GetGuidesHandler handler;
        public GetGuidesHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new GetGuidesHandler(context);
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(5, 5)]
        public async Task Handle_Should_Be_Success_When_Skip_Take(int skip, int take)
        {
            var request = new GetGuides
            {
                Skip = skip,
                Take = take
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Data.Length > 0);
        }

        [Theory]
        [InlineData(0, 5, "Test")]
        public async Task Handle_Should_Be_Success_When_Skip_Take_Filter(int skip, int take, string filter)
        {
            var request = new GetGuides
            {
                Skip = skip,
                Take = take,
                Filter = filter
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Collection(result.Data, x => x.Name.Contains(filter));
        }
    }
}
