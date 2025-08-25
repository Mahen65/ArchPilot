using Microsoft.AspNetCore.Mvc;
using MediatR;
using ArchPilot.Application.DTOs;
using ArchPilot.Application.Features.ProjectRequirements.Commands.CreateProjectRequirements;
using ArchPilot.Application.Features.ProjectRequirements.Queries.GetProjectRequirements;
using ArchPilot.Application.Services;

namespace ArchPilot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectRequirementsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IRecommendationEngine _recommendationEngine;

    public ProjectRequirementsController(IMediator mediator, IRecommendationEngine recommendationEngine)
    {
        _mediator = mediator;
        _recommendationEngine = recommendationEngine;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectRequirementsDto>> CreateProjectRequirements([FromBody] CreateProjectRequirementsCommand command)
    {
        try
        {
            // For demo purposes, set a default tenant ID
            if (string.IsNullOrEmpty(command.TenantId))
            {
                command.TenantId =Guid.NewGuid().ToString();
            }
            
            if (string.IsNullOrEmpty(command.UserId))
            {
                command.UserId = Guid.NewGuid().ToString();
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProjectRequirements), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating project requirements: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectRequirementsDto>> GetProjectRequirements(Guid id)
    {
        try
        {
            // For demo purposes, use a default tenant ID
            var query = new GetProjectRequirementsQuery(id, Guid.NewGuid());
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Project requirements with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving project requirements: {ex.Message}");
        }
    }

    [HttpPost("{id}/recommendations")]
    public async Task<ActionResult<List<ArchitectureRecommendationDto>>> GenerateRecommendations(Guid id, [FromQuery] int count = 3)
    {
        try
        {
            // Get the project requirements first
            var query = new GetProjectRequirementsQuery(id, Guid.NewGuid());
            var projectRequirements = await _mediator.Send(query);

            // Generate recommendations
            var recommendations = await _recommendationEngine.GenerateMultipleRecommendationsAsync(projectRequirements, count);
            
            return Ok(recommendations);
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

    [HttpGet("questionnaire/options")]
    public ActionResult<object> GetQuestionnaireOptions()
    {
        try
        {
            var options = new
            {
                ProjectTypes = Enum.GetValues<Domain.Enums.ProjectType>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                ProjectScales = Enum.GetValues<Domain.Enums.ProjectScale>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                ExpectedUsers = Enum.GetValues<Domain.Enums.ExpectedUsers>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                PerformancePriorities = Enum.GetValues<Domain.Enums.PerformancePriority>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                TechnologyExperiences = Enum.GetValues<Domain.Enums.TechnologyExperience>()
                    .Where(x => x != Domain.Enums.TechnologyExperience.None)
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                Timelines = Enum.GetValues<Domain.Enums.Timeline>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                BudgetRanges = Enum.GetValues<Domain.Enums.BudgetRange>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                RegionalCompliances = Enum.GetValues<Domain.Enums.RegionalCompliance>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                TrafficSpikeResponses = Enum.GetValues<Domain.Enums.TrafficSpikeResponse>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                TeamGrowthResponses = Enum.GetValues<Domain.Enums.TeamGrowthResponse>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                DataSensitivityResponses = Enum.GetValues<Domain.Enums.DataSensitivityResponse>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() }),
                IntegrationNeedsResponses = Enum.GetValues<Domain.Enums.IntegrationNeedsResponse>()
                    .Select(x => new { Value = (int)x, Name = x.ToString() })
            };

            return Ok(options);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving questionnaire options: {ex.Message}");
        }
    }
}
