using BloodLink.Application.Commands.CreateDonor;
using BloodLink.Application.Commands.UpdateDonor;
using BloodLink.Application.Queries.GetAllDonors;
using BloodLink.Application.Queries.GetDonorById;
using BloodLink.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodLink.API.Controllers
{
    [Route("api/donors")]
    public class DonorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DonorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/donors?query={query}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllDonorsQuery getAllDonorsQuery)
        {
            var donors = await _mediator.Send(getAllDonorsQuery);

            return Ok(donors);
        }

        // GET api/donors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDonorByIdQuery(id);

            var donor = await _mediator.Send(query);

            if (donor == null) return NotFound();

            return Ok(donor);
        }

        // POST api/donors
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDonorCommand command)
        {
            var createDonorResult = await _mediator.Send(command);

            if (!createDonorResult.IsSuccess)
                return BadRequest(new { createDonorResult.Message });

            return CreatedAtAction(nameof(GetById), new { id = createDonorResult.Data }, createDonorResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDonorCommand command)
        {
            if (command.Id != id)
                return BadRequest(new { Message = "ID in route does not match ID in body." });

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return NotFound(new { result.Message });

            return NoContent();
        }
    }
}

