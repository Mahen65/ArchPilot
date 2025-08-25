using ArchPilot.Domain.Common;

namespace ArchPilot.Domain.Entities;

public class ArchitectureRecommendation : BaseEntity
{
    public Guid ProjectRequirementsId { get; set; }
    public string TenantId { get; set; } = string.Empty;
    
    // Recommendation Details
    public string ArchitecturePattern { get; set; } = string.Empty;
    public string TechnologyStack { get; set; } = string.Empty;
    public string DatabaseRecommendation { get; set; } = string.Empty;
    public string InfrastructureRecommendation { get; set; } = string.Empty;
    public string SecurityRecommendations { get; set; } = string.Empty;
    public string ComplianceNotes { get; set; } = string.Empty;
    
    // Scoring and Justification
    public decimal OverallScore { get; set; }
    public string Justification { get; set; } = string.Empty;
    public string TradeOffs { get; set; } = string.Empty;
    public string Alternatives { get; set; } = string.Empty;
    
    // Implementation Details
    public string ImplementationRoadmap { get; set; } = string.Empty;
    public string EstimatedCost { get; set; } = string.Empty;
    public string EstimatedTimeline { get; set; } = string.Empty;
    
    // Navigation Properties
    public virtual ProjectRequirements ProjectRequirements { get; set; } = null!;
    public virtual ICollection<TechnologyStackItem> TechnologyStackItems { get; set; } = new List<TechnologyStackItem>();
}
