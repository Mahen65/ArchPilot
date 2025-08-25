using MediatR;
using ArchPilot.Application.DTOs;
using ArchPilot.Domain.Enums;

namespace ArchPilot.Application.Features.ProjectRequirements.Commands.CreateProjectRequirements;

public class CreateProjectRequirementsCommand : IRequest<ProjectRequirementsDto>
{
    public string TenantId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    
    // Core Questions
    public ProjectType ProjectType { get; set; }
    public ProjectScale ProjectScale { get; set; }
    public ExpectedUsers ExpectedUsers { get; set; }
    public PerformancePriority PerformancePriority { get; set; }
    public TechnologyExperience TeamExperience { get; set; }
    public Timeline Timeline { get; set; }
    public BudgetRange BudgetRange { get; set; }
    public RegionalCompliance RegionalCompliance { get; set; }
    
    // What If Scenarios
    public TrafficSpikeResponse TrafficSpikeResponse { get; set; }
    public TeamGrowthResponse TeamGrowthResponse { get; set; }
    public DataSensitivityResponse DataSensitivityResponse { get; set; }
    public IntegrationNeedsResponse IntegrationNeedsResponse { get; set; }
    
    // Additional Information
    public string? ProjectDescription { get; set; }
    public string? AdditionalNotes { get; set; }
}
