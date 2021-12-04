using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using SeturAssessment.Domain;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Xunit;

namespace SeturAssessment.Persistence.Test
{
    public class ContactTest : IClassFixture<DataModuleFixture>
    {
        private readonly IFixture fixture;
        private readonly SeturContext context;
        public ContactTest(DataModuleFixture dataModuleFixture)
        {
            fixture = dataModuleFixture.ServiceProvider.GetService<IFixture>();
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
        }

        [Theory]
        [InlineData("493d4da0-3d9a-4469-9d11-e4e72cebcbb3", "mail@mail.com", ContactType.EMAIL)]
        public void Model_Should_Be_Valid_When_ContacyType_Email(Guid id, string mail, ContactType contactType)
        {
            Assert.Equal(nameof(Guid), id.GetType().Name);
            Assert.NotEmpty(mail);
            Assert.Equal(ContactType.EMAIL, contactType);
            Assert.True(IsEmailAddress(mail));
            Assert.True(mail.Length <= 200);
        }

        [Theory]
        [InlineData("493d4da0-3d9a-4469-9d11-e4e72cebcbb3", "05431234567", ContactType.PHONE)]
        public void Model_Should_Be_Valid_When_ContacyType_Phone(Guid id, string phone, ContactType contactType)
        {
            Assert.Equal(nameof(Guid), id.GetType().Name);
            Assert.NotEmpty(phone);
            Assert.Equal(ContactType.PHONE, contactType);
            Assert.True(IsPhoneNumber(phone));
            Assert.True(phone.Length <= 200);
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
