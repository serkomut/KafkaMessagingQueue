using KafkaMessagingQueue.Persistence;
using KafkaMessagingQueue.Persistence.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using KafkaMessagingQueue.Messages.Commands;
using System;
using System.Threading;
using KafkaMessagingQueue.Domain;

namespace KafkaMessagingQueue.Commands.Test
{
    public class UpdateGuideHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly DatabaseContext context;
        private readonly UpdateGuideHandler handler;
        readonly Guide guide;
        public UpdateGuideHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<DatabaseContext>();
            handler = new UpdateGuideHandler(context);

            guide = new Guide
            {
                Name = "Name",
                Surname = "Surname",
                Company = "Company",
                CreateBy ="SYSTEM",
            };
            context.Guides.Add(guide);
            context.SaveChanges();
        }

        [Theory, Data]
        public async Task Handle_Should_Be_Exception_When_Item_Not_In_Database(UpdateGuide request)
        {
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Kayıt bulunamadı!", exception.Message);
        }

        [Fact]
        public async Task Handle_Should_Be_Success()
        {
            var request = new UpdateGuide
            {
                Id = guide.Id,
                Name = "New Name",
                Surname = "New Surname",
                UpdateBy = "SYSTEM"
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Aggregate != Guid.Empty);
        }
    }
}
