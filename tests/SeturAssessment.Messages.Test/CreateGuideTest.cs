using SeturAssessment.Messages.Commands;
using SeturAssessment.Messages.Models;
using System;
using System.Linq;
using Xunit;

namespace SeturAssessment.Messages.Test
{
    public class CreateGuideTest
    {
        [Fact]
        public void Guide_Validation_Should_InValid_When_Only_Model_Instance()
        {
            var command = new CreateGuide();
            var commandValidator = new CreateGuideValidator();
            var validator = commandValidator.Validate(command);
            Assert.False(validator.IsValid);
            Assert.Equal(2, validator.Errors.Count);
            
        }

        [Fact]
        public void Guide_Validation_Should_InValid_When_Name_Empty()
        {
            var command = new CreateGuide
            {
                Surname = "Surname",
                Company= "Company"
            };
            var commandValidator = new CreateGuideValidator();
            var validator = commandValidator.Validate(command);
            Assert.False(validator.IsValid);
            Assert.Collection(validator.Errors, x => x.ErrorMessage.Contains("'Name' must not be empty."));
        }

        [Fact]
        public void Guide_Validation_Should_InValid_When_Contacts()
        {
            var command = new CreateGuide
            {
                Name = "Name",
                Surname = "Surname",
                Company = "Company",
                Contacts = new[]
                {
                    new ContactModel()
                }
            };
            var commandValidator = new CreateGuideValidator();
            var validator = commandValidator.Validate(command);
            var items = validator.Errors.Select(x=> x.ErrorMessage).ToList();
            Assert.Equal("'Contact Type' must not be empty.", items[0]);
            Assert.Equal("'Value' must not be empty.", items[1]);
            Assert.False(validator.IsValid);
        }

        [Theory, Data]
        public void Guide_Validation_Should_Valid_When_Model_Instance(CreateGuide command)
        {
            command.Contacts.ToList().ForEach(x =>
            {
                if (x.ContactType == Models.ContactTypeEnum.EMAIL)
                    x.Value = "email@email.com";
                if (x.ContactType == Models.ContactTypeEnum.PHONE)
                    x.Value = "05551234567";
                if (x.ContactType == Models.ContactTypeEnum.LOCATION)
                    x.Value = "Antalya";
            });
            var commandValidator = new CreateGuideValidator();
            var validator = commandValidator.Validate(command);
            Assert.True(validator.IsValid);
        }
    }
}
