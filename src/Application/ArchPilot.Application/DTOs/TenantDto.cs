namespace ArchPilot.Application.DTOs;

public class TenantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime? SubscriptionExpiresAt { get; set; }
    public string SubscriptionPlan { get; set; } = "Free";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
