using AutoMapper;
using ArchPilot.Application.DTOs;
using ArchPilot.Application.Features.ProjectRequirements.Commands.CreateProjectRequirements;
using ArchPilot.Domain.Entities;

namespace ArchPilot.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ProjectRequirements mappings
        CreateMap<CreateProjectRequirementsCommand, ProjectRequirements>();
        CreateMap<ProjectRequirements, ProjectRequirementsDto>();
        CreateMap<ProjectRequirementsDto, ProjectRequirements>();

        // ArchitectureRecommendation mappings
        CreateMap<ArchitectureRecommendation, ArchitectureRecommendationDto>();
        CreateMap<ArchitectureRecommendationDto, ArchitectureRecommendation>();

        // TechnologyStackItem mappings
        CreateMap<TechnologyStackItem, TechnologyStackItemDto>();
        CreateMap<TechnologyStackItemDto, TechnologyStackItem>();

        // Tenant mappings
        CreateMap<Tenant, TenantDto>();
        CreateMap<TenantDto, Tenant>();

        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}
