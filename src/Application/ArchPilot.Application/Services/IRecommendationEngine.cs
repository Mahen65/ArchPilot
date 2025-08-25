using ArchPilot.Application.DTOs;

namespace ArchPilot.Application.Services;

public interface IRecommendationEngine
{
    Task<ArchitectureRecommendationDto> GenerateRecommendationAsync(ProjectRequirementsDto requirements);
    Task<List<ArchitectureRecommendationDto>> GenerateMultipleRecommendationsAsync(ProjectRequirementsDto requirements, int count = 3);
}
