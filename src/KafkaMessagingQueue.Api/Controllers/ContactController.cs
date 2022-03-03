using MediatR;
using Microsoft.AspNetCore.Mvc;
using KafkaMessagingQueue.Messages;
using KafkaMessagingQueue.Messages.Commands;
using KafkaMessagingQueue.Messages.Models;
using KafkaMessagingQueue.Messages.Queries;
using System;
using System.Threading.Tasks;

namespace KafkaMessagingQueue.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator mediator;

        public ContactController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{Id:guid}")]
        public Task<ContactDto> Get([FromRoute] GetContactById request) => mediator.Send(request);

        [HttpPost]
        public Task<CommandResponse<Guid>> Post([FromBody] AddContact request)
        {
            request.CreateBy = User.UserId();
            return mediator.Send(request);
        }

        [HttpPut("{id:guid}")]
        public Task<CommandResponse<Guid>> Put([FromRoute]Guid id, [FromBody] UpdateContact request)
        {
            request.Id = id;
            request.UpdateBy = User.UserId();
            return mediator.Send(request);
        }

        [HttpDelete("{Id:guid}")]
        public Task<CommandResponse<Guid>> Delete([FromRoute] DeleteContact request) => mediator.Send(request);
    }
}
