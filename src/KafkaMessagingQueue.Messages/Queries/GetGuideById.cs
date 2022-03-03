using MediatR;
using KafkaMessagingQueue.Messages.Models;
using System;

namespace KafkaMessagingQueue.Messages.Queries
{
    public record GetGuideById(Guid Id) : IRequest<GuideModel>;
}
