using MediatR;
using ArchPilot.Application.DTOs;
using ArchPilot.Application.Interfaces;
using ArchPilot.Application.Services;
using AutoMapper;

namespace ArchPilot.Application.Features.ProjectRequirements.Queries.GetRecommendation;

public class GetRecommendationQueryHandler : IRequestHandler<GetRecommendationQuery, List<ArchitectureRecommendationDto>>
{
    private readonly IArchPilotDbContext _context;
    private readonly IMapper _mapper;
    private readonly IRecommendationEngine _recommendationEngine;

    public GetRecommendationQueryHandler(IArchPilotDbContext context, IMapper mapper, IRecommendationEngine recommendationEngine)
    {
        _context = context;
        _mapper = mapper;
        _recommendationEngine = recommendationEngine;
    }

    public async Task<List<ArchitectureRecommendationDto>> Handle(GetRecommendationQuery request, CancellationToken cancellationToken)
    {
        var projectRequirements = await _context.ProjectRequirements
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (projectRequirements == null || projectRequirements.TenantId != request.TenantId)
        {
            throw new KeyNotFoundException($"Project requirements with ID {request.Id} not found.");
        }

        var projectRequirementsDto = _mapper.Map<ProjectRequirementsDto>(projectRequirements);
        var recommendations = await _recommendationEngine.GenerateMultipleRecommendationsAsync(projectRequirementsDto, request.Count);

        return recommendations;
    }
}
