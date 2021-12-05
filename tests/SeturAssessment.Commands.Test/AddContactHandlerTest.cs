using SeturAssessment.Persistence;
using SeturAssessment.Persistence.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using SeturAssessment.Messages.Commands;
using SeturAssessment.Domain;
using System;
using System.Threading;

namespace SeturAssessment.Commands.Test
{
    public class AddContactHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        private readonly AddContactHandler handler;
        Guid guidId;
        public AddContactHandlerTest(DataModuleFixture dataModuleFixture)
        {
            guidId = Guid.NewGuid();
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new AddContactHandler(context);
        }

        [Theory]
        [InlineData("6180d890-da06-4517-91cb-55d7f80e3053", "email@email.com", ContactType.EMAIL)]
        [InlineData("6180d890-da06-4517-91cb-55d7f80e3053", "05431234567", ContactType.PHONE)]
        public async Task Handle_Should_Be_Exception_When_Value_In_Database(Guid guideId, string value, ContactType contactType)
        {
            var entity = new Contact
            {
                GuideId = guideId,
                Value = value,
                ContactType = contactType,
                CreateBy = "SYSTEM",
                CreateDate = DateTime.Now
            };
            await context.Contacts.AddAsync(entity);
            await context.SaveChangesAsync();

            var request = new AddContact
            {
                ContactType = (Messages.Models.ContactTypeEnum)contactType,
                Value = entity.Value,
                CreateBy = "SYSTEM",
                GuideId = guideId
            };

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Bu kayıt daha önce eklenmiş!", exception.Message);
        }

        [Theory]
        [InlineData("3c8cf233-37e2-4382-aaee-d74dd1ad9940", "email@test.com", ContactType.EMAIL)]
        [InlineData("3c8cf233-37e2-4382-aaee-d74dd1ad9940", "05439874567", ContactType.PHONE)]
        [InlineData("3c8cf233-37e2-4382-aaee-d74dd1ad9940", "Antalya", ContactType.LOCATION)]
        public async Task Handle_Should_Be_Success_When_Request_Model(Guid guideId, string value, ContactType contactType)
        {
            var request = new AddContact
            {
                GuideId = guideId,
                Value = value,
                ContactType = (Messages.Models.ContactTypeEnum)contactType,
                CreateBy = Guid.NewGuid().ToString()
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Aggregate != Guid.Empty);
        }
    }
}
