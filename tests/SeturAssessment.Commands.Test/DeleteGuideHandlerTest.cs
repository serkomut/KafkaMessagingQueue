using SeturAssessment.Persistence;
using SeturAssessment.Persistence.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using SeturAssessment.Messages.Commands;
using System;
using System.Threading;
using SeturAssessment.Domain;
using Microsoft.EntityFrameworkCore;

namespace SeturAssessment.Commands.Test
{
    public class DeleteGuideHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        private readonly DeleteGuideHandler handler;
        public DeleteGuideHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new DeleteGuideHandler(context);
        }

        [Theory]
        [InlineData("6e939ec9-e369-4aaf-8484-3b161fdea797")]
        public async Task Handle_Should_Be_Exception_When_Item_Not_In_Database(Guid contactId)
        {
            var request = new DeleteGuide(contactId);

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Kayıt bulunamadı!", exception.Message);
        }

        [Theory, Data]
        public async Task Handle_Should_Be_Success(Guide entity)
        {
            await context.Guides.AddAsync(entity);
            await context.SaveChangesAsync();

            var request = new DeleteGuide(entity.Id);
            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Aggregate != Guid.Empty);
            Assert.Equal(entity.Id, result.Aggregate);
        }
    }
}
