using BloodLink.Application.Queries.GetAllBloodStocks;
using BloodLink.Application.Queries.GetDonationsReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodLink.API.Controllers
{
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBloodStocksQuery getAllBloodStocksQuery)
        {
            var bloodStock = await _mediator.Send(getAllBloodStocksQuery);

            return Ok(bloodStock);
        }

        [HttpGet("last-30-days")]
        public async Task<IActionResult> GetLast30Days()
        {
            var query = new GetDonationsReportQuery();
            var report = await _mediator.Send(query);

            return Ok(report);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var query = new GetBloodStockByIdQuery(id);

        //    var bloodStock = await _mediator.Send(query);

        //    return Ok(bloodStock);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] CreateBloodStockCommand command)
        //{
        //    var id = await _mediator.Send(command);

        //    return CreatedAtAction(nameof(GetById), new { id = id }, command);
        //}
    }
}
