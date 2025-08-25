using MediatR;
using ArchPilot.Application.DTOs;

namespace ArchPilot.Application.Features.ProjectRequirements.Queries.GetProjectRequirements;

public class GetProjectRequirementsQuery : IRequest<ProjectRequirementsDto>
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; } = Guid.Empty;

    public GetProjectRequirementsQuery(Guid id, Guid tenantId)
    {
        Id = id;
        TenantId = tenantId;
    }
}
