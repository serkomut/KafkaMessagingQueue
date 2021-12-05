using SeturAssessment.Persistence;
using SeturAssessment.Persistence.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using SeturAssessment.Messages.Commands;
using System;
using System.Threading;
using SeturAssessment.Domain;

namespace SeturAssessment.Commands.Test
{
    public class DeleteContactHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        private readonly DeleteContactHandler handler;
        public DeleteContactHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new DeleteContactHandler(context);
        }

        [Theory]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91")]
        public async Task Handle_Should_Be_Exception_When_Item_Not_In_Database(Guid contactId)
        {
            var request = new DeleteContact(contactId);

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Kayıt bulunamadı!", exception.Message);
        }

        [Theory]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "email@email.com", ContactType.EMAIL)]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "05431234567", ContactType.PHONE)]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "Antalya", ContactType.LOCATION)]
        public async Task Handle_Should_Be_Success(Guid guideId, string value, ContactType contactType)
        {
            var entity = new Contact
            {
                GuideId = guideId,
                Value = value,
                ContactType = contactType,
                CreateBy = Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now
            };
            await context.Contacts.AddAsync(entity);
            await context.SaveChangesAsync();

            var request = new DeleteContact(entity.Id);
            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Aggregate != Guid.Empty);
            Assert.Equal(entity.Id, result.Aggregate);
        }
    }
}
