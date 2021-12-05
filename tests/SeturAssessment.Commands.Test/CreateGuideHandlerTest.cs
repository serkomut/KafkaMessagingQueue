using FizzWare.NBuilder;
using SeturAssessment.Messages.Commands;
using SeturAssessment.Persistence;
using SeturAssessment.Persistence.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SeturAssessment.Commands.Test
{
    public class CreateGuideHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        private readonly CreateGuideHandler handler;
        public CreateGuideHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new CreateGuideHandler(context);
        }
        [Fact]
        public async Task Handle_Should_Be_Success_When_Create_Model()
        {
            var request = Builder<CreateGuide>.CreateNew().Build();
            
            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Aggregate != Guid.Empty);
        }
    }
}
