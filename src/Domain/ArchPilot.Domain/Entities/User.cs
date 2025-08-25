using ArchPilot.Domain.Common;
using ArchPilot.Domain.Enums;

namespace ArchPilot.Domain.Entities;

public class User : BaseEntity
{
    public Guid TenantId { get; set; } = Guid.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
    
    // Profile Information
    public string? JobTitle { get; set; }
    public string? Company { get; set; }
    public TechnologyExperience? PersonalExperience { get; set; }
    
    // Navigation Properties
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<ProjectRequirements> ProjectRequirements { get; set; } = new List<ProjectRequirements>();
}
