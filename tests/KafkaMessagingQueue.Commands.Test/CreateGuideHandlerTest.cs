using FizzWare.NBuilder;
using KafkaMessagingQueue.Messages.Commands;
using KafkaMessagingQueue.Persistence;
using KafkaMessagingQueue.Persistence.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace KafkaMessagingQueue.Commands.Test
{
    public class CreateGuideHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly DatabaseContext context;
        private readonly CreateGuideHandler handler;
        public CreateGuideHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<DatabaseContext>();
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
