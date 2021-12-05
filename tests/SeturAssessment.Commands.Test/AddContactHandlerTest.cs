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
        public AddContactHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new AddContactHandler(context);
        }

        [Theory]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "email@email.com", ContactType.EMAIL)]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "05431234567", ContactType.PHONE)]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "Antalya", ContactType.LOCATION)]
        public async Task Handle_Should_Be_Exception_When_Value_In_Database(Guid guideId, string value, ContactType contactType)
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

            var request = Builder<AddContact>.CreateNew().Build();
            request.Value = entity.Value;

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Bu kayıt daha önce eklenmiş!", exception.Message);
        }

        [Theory]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "email@email.com", ContactType.EMAIL)]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "05431234567", ContactType.PHONE)]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "Antalya", ContactType.LOCATION)]
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
