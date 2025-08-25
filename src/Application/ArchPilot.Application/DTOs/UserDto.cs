using ArchPilot.Domain.Enums;

namespace ArchPilot.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
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
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
