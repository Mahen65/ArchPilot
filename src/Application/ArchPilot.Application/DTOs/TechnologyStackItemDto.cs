namespace ArchPilot.Application.DTOs;

public class TechnologyStackItemDto
{
    public Guid Id { get; set; }
    public Guid ArchitectureRecommendationId { get; set; }
    public Guid TenantId { get; set; } = Guid.Empty;
    
    public string Category { get; set; } = string.Empty;
    public string Technology { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public string Justification { get; set; } = string.Empty;
    public bool IsRequired { get; set; } = true;
    public int Priority { get; set; } = 1;
}
