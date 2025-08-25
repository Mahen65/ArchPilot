using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ArchPilot.Domain.Entities;
using ArchPilot.Application.Interfaces;

namespace ArchPilot.Persistence.Data;

public class ArchPilotDbContext : IdentityDbContext, IArchPilotDbContext
{
    public ArchPilotDbContext(DbContextOptions<ArchPilotDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProjectRequirements> ProjectRequirements { get; set; }
    public DbSet<ArchitectureRecommendation> ArchitectureRecommendations { get; set; }
    public DbSet<TechnologyStackItem> TechnologyStackItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entity relationships and constraints
        ConfigureTenant(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureProjectRequirements(modelBuilder);
        ConfigureArchitectureRecommendation(modelBuilder);
        ConfigureTechnologyStackItem(modelBuilder);

        // Add multi-tenant filtering
        ConfigureMultiTenancy(modelBuilder);
    }

    private void ConfigureTenant(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Domain).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ContactEmail).IsRequired().HasMaxLength(255);
            entity.Property(e => e.SubscriptionPlan).HasMaxLength(50);
            entity.HasIndex(e => e.Domain).IsUnique();
        });
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.JobTitle).HasMaxLength(100);
            entity.Property(e => e.Company).HasMaxLength(200);

            entity.HasOne(e => e.Tenant)
                  .WithMany(t => t.Users)
                  .HasForeignKey(e => e.TenantId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.Email).IsUnique();
        });
    }

    private void ConfigureProjectRequirements(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectRequirements>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProjectDescription).HasMaxLength(1000);
            entity.Property(e => e.AdditionalNotes).HasMaxLength(2000);

            entity.HasMany(e => e.Recommendations)
                  .WithOne(r => r.ProjectRequirements)
                  .HasForeignKey(r => r.ProjectRequirementsId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureArchitectureRecommendation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArchitectureRecommendation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ArchitecturePattern).IsRequired().HasMaxLength(200);
            entity.Property(e => e.TechnologyStack).HasMaxLength(1000);
            entity.Property(e => e.DatabaseRecommendation).HasMaxLength(500);
            entity.Property(e => e.InfrastructureRecommendation).HasMaxLength(1000);
            entity.Property(e => e.SecurityRecommendations).HasMaxLength(1000);
            entity.Property(e => e.ComplianceNotes).HasMaxLength(1000);
            entity.Property(e => e.Justification).HasMaxLength(2000);
            entity.Property(e => e.TradeOffs).HasMaxLength(2000);
            entity.Property(e => e.Alternatives).HasMaxLength(2000);
            entity.Property(e => e.ImplementationRoadmap).HasMaxLength(3000);
            entity.Property(e => e.EstimatedCost).HasMaxLength(200);
            entity.Property(e => e.EstimatedTimeline).HasMaxLength(200);
            entity.Property(e => e.OverallScore).HasPrecision(5, 2);

            entity.HasMany(e => e.TechnologyStackItems)
                  .WithOne(t => t.ArchitectureRecommendation)
                  .HasForeignKey(t => t.ArchitectureRecommendationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureTechnologyStackItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TechnologyStackItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Technology).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Version).HasMaxLength(50);
            entity.Property(e => e.Purpose).HasMaxLength(500);
            entity.Property(e => e.Justification).HasMaxLength(1000);
        });
    }

    private void ConfigureMultiTenancy(ModelBuilder modelBuilder)
    {
        // Add global query filters for multi-tenancy
        modelBuilder.Entity<User>().HasQueryFilter(e => e.TenantId == GetCurrentTenantId());
        modelBuilder.Entity<ProjectRequirements>().HasQueryFilter(e => e.TenantId == GetCurrentTenantId());
        modelBuilder.Entity<ArchitectureRecommendation>().HasQueryFilter(e => e.TenantId == GetCurrentTenantId());
        modelBuilder.Entity<TechnologyStackItem>().HasQueryFilter(e => e.TenantId == GetCurrentTenantId());
    }

    private string GetCurrentTenantId()
    {
        // This will be implemented with a tenant provider service
        return string.Empty;
    }
}
