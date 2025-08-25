using Microsoft.EntityFrameworkCore;
using ArchPilot.Domain.Entities;

namespace ArchPilot.Application.Interfaces;

public interface IArchPilotDbContext
{
    DbSet<Tenant> Tenants { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<ProjectRequirements> ProjectRequirements { get; set; }
    DbSet<ArchitectureRecommendation> ArchitectureRecommendations { get; set; }
    DbSet<TechnologyStackItem> TechnologyStackItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
