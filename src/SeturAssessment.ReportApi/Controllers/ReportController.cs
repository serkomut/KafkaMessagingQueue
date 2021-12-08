using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeturAssessment.ReportApi.Application.Models;
using SeturAssessment.ReportApi.Application.Queries;
using System.Threading.Tasks;

namespace SeturAssessment.ReportApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetReports")]
        public Task<ReportModel[]> Get([FromQuery]GetReports request) => mediator.Send(request);

        [HttpGet("{Id:guid}")]
        public Task<ReportModel> Get([FromRoute] GetReportById request) => mediator.Send(request);

    }
}