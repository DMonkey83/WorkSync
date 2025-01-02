using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueTypeController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IssueTypeController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueTypeDto>>> GetAllIssueTypes()
        {
            var issueTypes = await _context.IssueTypes.ToListAsync();
            return _mapper.Map<List<IssueTypeDto>>(issueTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueTypeDto>> GetIssueTypeById(Guid id)
        {
            var issueType = await _context.IssueTypes.FindAsync(id);
            if (issueType == null)
            {
                return NotFound();
            }
            return _mapper.Map<IssueTypeDto>(issueType);
        }

        [HttpPost]
        public async Task<ActionResult<IssueTypeDto>> CreateIssueType(CreateIssueTypeDto createIssueTypeDto)
        {
            var issueType = _mapper.Map<IssueType>(createIssueTypeDto);
            _context.IssueTypes.Add(issueType);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue type");
            }
            return CreatedAtAction(nameof(GetIssueTypeById), new { id = issueType.Id }, _mapper.Map<IssueTypeDto>(issueType));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssueTypeDto>> UpdateIssueType(Guid id, UpdateIssueTypeDto updateIssueTypeDto)
        {
            var issueType = await _context.IssueTypes.FindAsync(id);
            if (issueType == null)
            {
                return NotFound();
            }
            
            issueType.IssueTypeName = updateIssueTypeDto.TypeName ?? issueType.IssueTypeName;
            issueType.Description = updateIssueTypeDto.Description ?? issueType.Description;
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue type");
            }
            return _mapper.Map<IssueTypeDto>(issueType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIssueType(Guid id)
        {
            var issueType = await _context.IssueTypes.FindAsync(id);
            if (issueType == null)
            {
                return NotFound();
            }
            _context.IssueTypes.Remove(issueType);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete issue type");
            }
            return NoContent();
        }
    }
}