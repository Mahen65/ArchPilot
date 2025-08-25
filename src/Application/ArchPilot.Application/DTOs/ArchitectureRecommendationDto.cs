namespace ArchPilot.Application.DTOs;

public class ArchitectureRecommendationDto
{
    public Guid Id { get; set; }
    public Guid ProjectRequirementsId { get; set; }
    public Guid TenantId { get; set; } = Guid.Empty;
    
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
    
    public DateTime CreatedAt { get; set; }
    public List<TechnologyStackItemDto> TechnologyStackItems { get; set; } = new();
}
