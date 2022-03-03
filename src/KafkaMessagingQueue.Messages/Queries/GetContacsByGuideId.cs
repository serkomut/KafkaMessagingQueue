using MediatR;
using KafkaMessagingQueue.Messages.Models;
using System;

namespace KafkaMessagingQueue.Messages.Queries
{
    public record GetContacsByGuideId(Guid GuideId) : IRequest<ContactDto[]>;
}
