using MediatR;
using AutoMapper;
using ArchPilot.Application.DTOs;
using ArchPilot.Application.Interfaces;
using ArchPilot.Domain.Entities;

namespace ArchPilot.Application.Features.ProjectRequirements.Commands.CreateProjectRequirements;

public class CreateProjectRequirementsCommandHandler : IRequestHandler<CreateProjectRequirementsCommand, ProjectRequirementsDto>
{
    private readonly IArchPilotDbContext _context;
    private readonly IMapper _mapper;

    public CreateProjectRequirementsCommandHandler(IArchPilotDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectRequirementsDto> Handle(CreateProjectRequirementsCommand request, CancellationToken cancellationToken)
    {
        var projectRequirements = _mapper.Map<Domain.Entities.ProjectRequirements>(request);
        
        _context.ProjectRequirements.Add(projectRequirements);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProjectRequirementsDto>(projectRequirements);
    }
}
