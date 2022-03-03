using MediatR;
using KafkaMessagingQueue.Messages.Models;
using System;

namespace KafkaMessagingQueue.Messages.Commands
{
    public record DeleteContact(Guid Id) : IRequest<CommandResponse<Guid>>;
}
