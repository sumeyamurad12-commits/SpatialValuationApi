using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpatialValuation.Application.Valuations.Queries.CalculatePropertyValuation;
using SpatialValuation.Application.Valuations.Commands.UploadPropertyDocument;

namespace SpatialValuation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValuationsController : ControllerBase
{
    private readonly ISender _mediator;

    public ValuationsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("calculate")]
    public async Task<ActionResult<ValuationResultDto>> CalculateValuation(
        [FromBody] CalculatePropertyValuationQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("{id:guid}/documents")]
    [Consumes("multipart/form-data")] // Required for IFormFile file uploads in OpenAPI / Swagger
    public async Task<IActionResult> UploadDocument(
    Guid id,
    [FromForm] IFormFile legalDocument,
    [FromForm] IFormFile? spatialVectorFile)
    {
        var command = new UploadPropertyDocumentCommand(id, legalDocument, spatialVectorFile);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}