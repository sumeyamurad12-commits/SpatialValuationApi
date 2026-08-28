using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpatialValuation.Application.Properties.Commands.CreateProperty;
using SpatialValuation.Application.Properties.Queries.GetNearbyProperties;
using SpatialValuation.Domain.Entities;
using SpatialValuation.Application.Properties.Queries.CalculateValuation;
using SpatialValuation.Application.Properties.Queries.GetPropertyById;

namespace SpatialValuation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly ISender _mediator;

    public PropertiesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePropertyCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id }, id);
    }

    [HttpGet("nearby")]
    public async Task<ActionResult<List<NearbyPropertyDto>>> GetNearby(
    [FromQuery] double longitude,
    [FromQuery] double latitude,
    [FromQuery] double distanceInMeters = 1000)
    {
        var query = new GetNearbyPropertiesQuery(longitude, latitude, distanceInMeters);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("calculate-valuation")]
    public async Task<ActionResult<ValuationResultDto>> CalculateValuation(
    [FromBody] CalculateValuationQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPropertyByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Property Not Found",
                Detail = $"Property with ID '{id}' was not found in the spatial database."
            });
        }

        return Ok(result);
    }
}