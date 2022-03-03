using MediatR;
using KafkaMessagingQueue.Messages.Models;
using System;

namespace KafkaMessagingQueue.Messages.Commands
{
    public record DeleteGuide(Guid Id) : IRequest<CommandResponse<Guid>>;
}
