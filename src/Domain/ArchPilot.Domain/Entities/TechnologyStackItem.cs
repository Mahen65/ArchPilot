using ArchPilot.Domain.Common;

namespace ArchPilot.Domain.Entities;

public class TechnologyStackItem : BaseEntity
{
    public Guid ArchitectureRecommendationId { get; set; }
    public Guid TenantId { get; set; } = Guid.Empty;

    public string Category { get; set; } = string.Empty; // Frontend, Backend, Database, Infrastructure, etc.
    public string Technology { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public string Justification { get; set; } = string.Empty;
    public bool IsRequired { get; set; } = true;
    public int Priority { get; set; } = 1;
    
    // Navigation Properties
    public virtual ArchitectureRecommendation ArchitectureRecommendation { get; set; } = null!;
}
