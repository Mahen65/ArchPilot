using ArchPilot.Domain.Common;
using ArchPilot.Domain.Enums;

namespace ArchPilot.Domain.Entities;

public class ProjectRequirements : BaseEntity
{
    public Guid TenantId { get; set; } = Guid.Empty;
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
    
    // Navigation Properties
    public virtual ICollection<ArchitectureRecommendation> Recommendations { get; set; } = new List<ArchitectureRecommendation>();
}
