using SeturAssessment.Messages.Commands;
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
