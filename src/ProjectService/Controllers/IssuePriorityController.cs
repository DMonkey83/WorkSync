using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/issue-priorities")]
    public class IssuePriorityController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IssuePriorityController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<IssuePriorityDto>>> GetAllIssuePriorities()
        {
            var issuePriorities = await _context.IssuePriorities
                .OrderBy(s => s.PriorityName)
                .ToListAsync();
            return _mapper.Map<List<IssuePriorityDto>>(issuePriorities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssuePriorityDto>> GetIssuePriorityById(Guid id)
        {
            var issuePriority = await _context.IssuePriorities
                .FirstOrDefaultAsync(s => s.Id == id);
            if (issuePriority == null)
            {
                return NotFound();
            }
            var issuePriorityDto = _mapper.Map<IssuePriorityDto>(issuePriority);
            return Ok(issuePriorityDto);
        }

        [HttpPost]
        public async Task<ActionResult<IssuePriorityDto>> CreateIssuePriority(CreateIssuePriorityDto createIssuePriorityDto)
        {
            var issuePriority = _mapper.Map<IssuePriority>(createIssuePriorityDto);
            _context.IssuePriorities.Add(issuePriority);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue priority");
            }
            return CreatedAtAction(nameof(GetIssuePriorityById), new { id = issuePriority.Id }, _mapper.Map<IssuePriorityDto>(issuePriority));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssuePriorityDto>> UpdateIssuePriority(Guid id, UpdateIssuePriorityDto updateIssuePriorityDto)
        {
            var issuePriority = await _context.IssuePriorities.FindAsync(id);
            if (issuePriority == null)
            {
                return NotFound();
            }

            issuePriority.PriorityName = updateIssuePriorityDto.PriorityName ?? issuePriority.PriorityName;

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue priority");
            }
            return _mapper.Map<IssuePriorityDto>(issuePriority);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIssuePriority(Guid id)
        {
            var issuePriority = await _context.IssuePriorities.FindAsync(id);
            if (issuePriority == null)
            {
                return NotFound();
            }
            _context.IssuePriorities.Remove(issuePriority);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete issue priority");
            }
            return Ok();
        }
    }
}