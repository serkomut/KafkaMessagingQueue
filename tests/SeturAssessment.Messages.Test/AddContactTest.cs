using SeturAssessment.Messages.Commands;
using Xunit;

namespace SeturAssessment.Messages.Test
{
    public class AddContactTest
    {
        [Fact]
        public void Contact_Validation_Should_Be_InValid_When_AModel_Instance()
        {
            var command = new AddContact();
            var commandValidator = new AddContactValidator();
            var validator = commandValidator.Validate(command);
            Assert.False(validator.IsValid);
            Assert.Contains(validator.Errors, x => x.ErrorMessage.Equals("'Contact Type' must not be empty."));
            Assert.Contains(validator.Errors, x => x.ErrorMessage.Equals("'Value' must not be empty."));
        }

        [Theory, Data]
        public void Contact_Validation_Should_Be_Valid_Model_Instance(AddContact command)
        {
            command.Value = "test@test.com";
            var commandValidator = new AddContactValidator();
            var validator = commandValidator.Validate(command);
            Assert.True(validator.IsValid);
        }
    }
}
