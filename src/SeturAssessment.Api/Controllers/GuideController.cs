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
    public class GuideController : ControllerBase
    {
        private readonly IMediator mediator;
        public GuideController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<QueryableResponse<GuideModel>> Get([FromQuery] GetGuides request) => mediator.Send(request);

        [HttpGet("{Id:guid}")]
        public Task<GuideModel> Get([FromRoute] GetGuideById request) => mediator.Send(request);

        [HttpGet("{GuideId:guid}/contacts")]
        public Task<ContactDto[]> Get([FromRoute] GetContacsByGuideId request) => mediator.Send(request);

        [HttpPost]
        public Task<CommandResponse<Guid>> Post([FromBody] CreateGuide request)
        {
            request.CreateBy = User.UserId();
            return mediator.Send(request);
        }

        [HttpPut("{id:guid}")]
        public Task<CommandResponse<Guid>> Put(Guid id, [FromBody] UpdateGuide request)
        {
            request.Id = id;
            request.UpdateBy = User.UserId();
            return mediator.Send(request);
        }

        [HttpDelete("{Id:guid}")]
        public Task<CommandResponse<Guid>> Delete([FromRoute] DeleteGuide request) => mediator.Send(request);
    }
}
