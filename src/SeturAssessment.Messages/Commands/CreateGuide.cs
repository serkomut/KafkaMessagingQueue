using FluentValidation;
using MediatR;
using SeturAssessment.Messages.Models;
using System.Text.RegularExpressions;

namespace SeturAssessment.Messages.Commands
{
    public class CreateGuide : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactModel[] Contacts { get; set; }
    }

    public class CreateGuideValidator : AbstractValidator<CreateGuide>
    {
        public CreateGuideValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Contacts)
                .ForEach(x => x.SetValidator(new ContactModelValidator()))
                .When(x => x.Contacts != null);
        }
    }

    public class ContactModelValidator : AbstractValidator<ContactModel>
    {
        public ContactModelValidator()
        {
            RuleFor(x => x.ContactType).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Value).NotEmpty()
                .EmailAddress()
                .When(x => x.ContactType == ContactTypeEnum.EMAIL);

            RuleFor(x => x.Value).NotEmpty()
                .Must(x=> IsPhoneNumber(x))
                .When(x => x.ContactType == ContactTypeEnum.PHONE);
        }

        private static bool IsPhoneNumber(string value)
        {
            const string desen = @"^(05(\d{9}))$";
            var match = Regex.Match(value, desen, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
