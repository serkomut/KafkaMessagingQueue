using MediatR;
using KafkaMessagingQueue.Messages.Models;
using System;

namespace KafkaMessagingQueue.Messages.Queries
{
    public record GetContactById(Guid Id) : IRequest<ContactDto>;
}
