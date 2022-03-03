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
    public class UpdateContactHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly DatabaseContext context;
        private readonly UpdateContactHandler handler;
        readonly Contact contact;
        public UpdateContactHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<DatabaseContext>();
            handler = new UpdateContactHandler(context);

            contact = new Contact
            {
                ContactType = ContactType.EMAIL,
                Value = "email@m.com",
                CreateBy = "SYSTEM",
                GuideId = Guid.NewGuid()
            };
            context.Contacts.Add(contact);
            context.SaveChanges();
        }

        [Theory, Data]
        public async Task Handle_Should_Be_Exception_When_Item_Not_In_Database(UpdateContact request)
        {
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Kayıt bulunamadı!", exception.Message);
        }

        [Fact]
        public async Task Handle_Should_Be_Existing_Record_Exception_When_Item_In_Database()
        {
            var model = new Contact
            {
                ContactType = ContactType.EMAIL,
                Value = "email@m2.com",
                CreateBy = "SYSTEM",
                GuideId = Guid.NewGuid()
            };
            await context.Contacts.AddAsync(model);
            await context.SaveChangesAsync();

            var request = new UpdateContact
            {
                ContactType = (Messages.Models.ContactTypeEnum)model.ContactType,
                Id = model.Id,
                Value = contact.Value,
                UpdateBy = "SYSTEM"
            };


            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Bu kayıt daha önce eklenmiş!", exception.Message);
        }

        [Fact]
        public async Task Handle_Should_Be_Success()
        {
            var request = new UpdateContact
            {
                ContactType = (Messages.Models.ContactTypeEnum)contact.ContactType,
                Id = contact.Id,
                Value = "newemail@new.com",
                UpdateBy = "SYSTEM"
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Aggregate != Guid.Empty);
        }
    }
}
