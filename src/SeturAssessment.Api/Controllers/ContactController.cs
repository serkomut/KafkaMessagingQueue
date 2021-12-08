using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeturAssessment.Messages;
using SeturAssessment.Messages.Commands;
using SeturAssessment.Messages.Models;
using SeturAssessment.Messages.Queries;
using System;
using System.Threading.Tasks;

namespace SeturAssessment.Api.Controllers
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

        [HttpGet("{id:guid}")]
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

        [HttpDelete("{id:guid}")]
        public Task<CommandResponse<Guid>> Delete([FromRoute] DeleteContact request) => mediator.Send(request);
    }
}
