using FluentValidation;
using MediatR;
using System;

namespace SeturAssessment.Messages.Commands
{
    public class UpdateGuide : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }

    public class UpdateGuideValidator : AbstractValidator<UpdateGuide>
    {
        public UpdateGuideValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}
