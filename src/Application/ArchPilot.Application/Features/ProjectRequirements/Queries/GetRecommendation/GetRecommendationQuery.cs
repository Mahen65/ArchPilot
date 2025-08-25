using MediatR;
using ArchPilot.Application.DTOs;

namespace ArchPilot.Application.Features.ProjectRequirements.Queries.GetRecommendation;

public class GetRecommendationQuery : IRequest<List<ArchitectureRecommendationDto>>
{
    public Guid Id { get; set; }
    public string TenantId { get; set; }
    public int Count { get; set; }

    public GetRecommendationQuery(Guid id, string tenantId, int count)
    {
        Id = id;
        TenantId = tenantId;
        Count = count;
    }
}
