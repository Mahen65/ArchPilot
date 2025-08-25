using MediatR;
using ArchPilot.Application.DTOs;

namespace ArchPilot.Application.Features.ProjectRequirements.Queries.GetProjectRequirements;

public class GetProjectRequirementsQuery : IRequest<ProjectRequirementsDto>
{
    public Guid Id { get; set; }
    public string TenantId { get; set; } = string.Empty;

    public GetProjectRequirementsQuery(Guid id, string tenantId)
    {
        Id = id;
        TenantId = tenantId;
    }
}
