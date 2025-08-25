using MediatR;
using AutoMapper;
using ArchPilot.Application.DTOs;
using ArchPilot.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArchPilot.Application.Features.ProjectRequirements.Queries.GetProjectRequirements;

public class GetProjectRequirementsQueryHandler : IRequestHandler<GetProjectRequirementsQuery, ProjectRequirementsDto>
{
    private readonly IArchPilotDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectRequirementsQueryHandler(IArchPilotDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectRequirementsDto> Handle(GetProjectRequirementsQuery request, CancellationToken cancellationToken)
    {
        var projectRequirements = await _context.ProjectRequirements
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.TenantId == request.TenantId, cancellationToken);

        if (projectRequirements == null)
        {
            throw new KeyNotFoundException($"Project requirements with ID {request.Id} not found.");
        }

        return _mapper.Map<ProjectRequirementsDto>(projectRequirements);
    }
}
