using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/sprints")]
    public class SprintController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public SprintController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SprintDto>>> GetAllSprints()
        {
            var sprints = await _context.Sprints
                .Include(x => x.Project)
                .OrderBy(s => s.SprintName)
                .ToListAsync();
            return _mapper.Map<List<SprintDto>>(sprints);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SprintDto>> GetSprintById(Guid id)
        {
            var sprint = await _context.Sprints
                .Include(x => x.Project)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sprint == null)
            {
                return NotFound();
            }
            var sprintDto = _mapper.Map<SprintDto>(sprint);
            return Ok(sprintDto);
        }

        [HttpPost]
        public async Task<ActionResult<SprintDto>> CreateSprint(CreateSprintDto createSprintDto)
        {
            var sprint = _mapper.Map<Sprint>(createSprintDto);
            _context.Sprints.Add(sprint);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create sprint");
            }
            var createdSprint = await _context.Sprints
                .Include(x => x.Project)
                .FirstOrDefaultAsync(s => s.Id == sprint.Id);
            return CreatedAtAction(nameof(GetSprintById), new { id = sprint.Id }, _mapper.Map<SprintDto>(createdSprint));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SprintDto>> UpdateSprint(Guid id, UpdateSprintDto updateSprintDto)
        {
            var sprint = await _context.Sprints
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sprint == null)
            {
                return NotFound();
            }

            sprint.SprintName = updateSprintDto.SprintName ?? sprint.SprintName;
            sprint.StartDate = updateSprintDto.StartDate ?? sprint.StartDate;
            sprint.EndDate = updateSprintDto.EndDate ?? sprint.EndDate;
            sprint.Goal = updateSprintDto.Goal ?? sprint.Goal;
            sprint.Status = Enum.Parse<SprintStatus>(updateSprintDto.Status ?? sprint.Status.ToString());

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update sprint");
            }
            return _mapper.Map<SprintDto>(sprint);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSprint(Guid id)
        {
            var sprint = await _context.Sprints
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sprint == null)
            {
                return NotFound();
            }
            _context.Sprints.Remove(sprint);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete sprint");
            }
            return Ok();
        }
    }
}
