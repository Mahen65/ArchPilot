using ArchPilot.Domain.Enums;

namespace ArchPilot.Application.DTOs;

public class ProjectRequirementsDto
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; } = Guid.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    
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
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
