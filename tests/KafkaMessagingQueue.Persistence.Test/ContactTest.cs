using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using KafkaMessagingQueue.Domain;
using KafkaMessagingQueue.Persistence.Test.Infrastructure;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace KafkaMessagingQueue.Persistence.Test
{
    public class ContactTest : IClassFixture<DataModuleFixture>
    {
        private readonly IFixture fixture;
        private readonly DatabaseContext context;
        public ContactTest(DataModuleFixture dataModuleFixture)
        {
            fixture = dataModuleFixture.ServiceProvider.GetService<IFixture>();
            context = dataModuleFixture.ServiceProvider.GetService<DatabaseContext>();
        }

        [Theory]
        [InlineData("mail@mail.com", ContactType.EMAIL)]
        public void Model_Should_Be_Valid_When_ContacyType_Email(string mail, ContactType contactType)
        {
            Assert.NotEmpty(mail);
            Assert.Equal(ContactType.EMAIL, contactType);
            Assert.True(IsEmailAddress(mail));
            Assert.True(mail.Length <= 200);
        }

        [Theory]
        [InlineData("05431234567", ContactType.PHONE)]
        public void Model_Should_Be_Valid_When_ContacyType_Phone(string phone, ContactType contactType)
        {
            Assert.NotEmpty(phone);
            Assert.Equal(ContactType.PHONE, contactType);
            Assert.True(IsPhoneNumber(phone));
            Assert.True(phone.Length <= 200);
        }

        [Theory]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "email@email.com", ContactType.EMAIL)]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "05431234567", ContactType.PHONE)]
        [InlineData("f08bf843-abfc-43e4-8ce6-d597f2cccc91", "Antalya", ContactType.LOCATION)]
        public async Task Creation_Should_Success_When_Any_Entity(Guid guideId, string value, ContactType contactType)
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
            var result = await context.SaveChangesAsync();
            Assert.True(result > 0);
        }

        [Theory, Data]
        public async Task Update_Should_Success_When_An_Entity(Contact entity)
        {
            await context.Contacts.AddAsync(entity);
            await context.SaveChangesAsync();

            var item = context.Contacts.FirstOrDefault(x => x.Id == entity.Id);

            item.Value = "New Value";
            context.Contacts.Update(item);
            var result = await context.SaveChangesAsync();
            Assert.True(result > 0);
            Assert.Equal(entity.Value, item.Value);
        }

        [Theory, Data]
        public async Task Delete_Should_Success_When_An_Entity(Contact entity)
        {
            await context.Contacts.AddAsync(entity);
            await context.SaveChangesAsync();

            var item = context.Contacts.FirstOrDefault(x => x.Id == entity.Id);
            context.Contacts.Remove(item);
            var result = await context.SaveChangesAsync();
            var exists = context.Contacts.Any(x => x.Id == entity.Id);
            Assert.False(exists);
            Assert.True(result > 0);
        }

        public static bool IsPhoneNumber(string value)
        {
            const string desen = @"^(05(\d{9}))$";
            var match = Regex.Match(value, desen, RegexOptions.IgnoreCase);
            return match.Success;
        }

        private static bool IsEmailAddress(string value)
        {
            try
            {
                var result = new MailAddress(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
