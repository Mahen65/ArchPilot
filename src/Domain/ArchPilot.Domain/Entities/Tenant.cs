using ArchPilot.Domain.Common;

namespace ArchPilot.Domain.Entities;

public class Tenant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime? SubscriptionExpiresAt { get; set; }
    public string SubscriptionPlan { get; set; } = "Free";
    
    // Settings
    public string Settings { get; set; } = "{}"; // JSON settings
    
    // Navigation Properties
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<ProjectRequirements> ProjectRequirements { get; set; } = new List<ProjectRequirements>();
}
