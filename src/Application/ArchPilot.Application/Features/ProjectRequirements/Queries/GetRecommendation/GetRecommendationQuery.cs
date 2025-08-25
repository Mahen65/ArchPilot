using MediatR;
using ArchPilot.Application.DTOs;

namespace ArchPilot.Application.Features.ProjectRequirements.Queries.GetRecommendation;

public class GetRecommendationQuery : IRequest<List<ArchitectureRecommendationDto>>
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public int Count { get; set; }

    public GetRecommendationQuery(Guid id, Guid tenantId, int count)
    {
        Id = id;
        TenantId = tenantId;
        Count = count;
    }
}
