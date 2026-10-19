using BloodLink.Application.Commands.CreateDonation;
using BloodLink.Application.Queries.GetAllDonation;
using BloodLink.Application.Queries.GetDonationById;
using BloodLink.Application.Queries.GetDonationsByDonorId;
using BloodLink.Application.Queries.GetRecentDonations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodLink.API.Controllers
{
    [Route("api/donation")]
    public class DonationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DonationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllDonationsQuery query)
        {
            var donation = await _mediator.Send(query);

            return Ok(donation);
        }

        //// GET api/donations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var query = new GetDonationByIdQuery(id);

            var donation = await _mediator.Send(query);

            if (donation == null) return NotFound();

            return Ok(donation);
        }

        //// GET api/donations/{id}
        [HttpGet("by-donorId")]
        public async Task<IActionResult> GetDonationsByDonorId([FromQuery] GetDonationsByDonorIdQuery query)
        {           
            var donations = await _mediator.Send(query);

            return Ok(donations);
        }

        [HttpGet("recentDonations")]
        public async Task<IActionResult> GetRecentDonations()
        {
            var query = new GetRecentDonationsQuery();
            var recentDonations = await _mediator.Send(query);
            return Ok(recentDonations);
        }

        // POST api/donations
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDonationCommand command)
        {
            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
                return BadRequest(new { result.Message });


            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }
    }
}
