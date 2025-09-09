using MediatR;
using Microsoft.AspNetCore.Mvc;
using ArchPilot.Application.Features.ProjectRequirements.Queries.GetRecommendation;

namespace ArchPilot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecommendationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}/{TenantId}/{Count}")]
    public async Task<IActionResult> GetRecommendations(Guid  id,Guid TenantId,int Count)
    {
        try
        {
            var query = new GetRecommendationQuery(id, TenantId, Count);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Project requirements with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error generating recommendations: {ex.Message}");
        }
    }
}
