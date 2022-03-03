using FluentValidation;
using MediatR;
using KafkaMessagingQueue.Messages.Models;
using System;

namespace KafkaMessagingQueue.Messages.Commands
{
    public class UpdateGuide : IRequest<CommandResponse<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string UpdateBy { get; set; }
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
