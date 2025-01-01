using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/issues")]
    public class IssueController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IssueController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueDto>>> GetAllIssues()
        {
            var issues = await _context.Issues
            .Include(x => x.IssuePriority)
            .Include(x => x.IssueStatus)
            .Include(x => x.Component)
            .Include(x => x.Sprint)
            .Include(x => x.Project)
            .Include(x => x.IssueType)
            .OrderBy(i => i.IssueKey)
            .ToListAsync();
            return _mapper.Map<List<IssueDto>>(issues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueDto>> GetIssueById(Guid id)
        {
            var issue = await _context.Issues
                .Include(x => x.IssuePriority)
                .Include(x => x.IssueStatus)
                .Include(x => x.IssueType)
            .FirstOrDefaultAsync(i => i.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            var issueDto = _mapper.Map<IssueDto>(issue);
            return Ok(issueDto);
        }
        [HttpGet("key/{key}")]
        public async Task<IActionResult> GetIssueByKey(string key)
        {
            var issue = await _context.Issues
                .Include(x => x.IssuePriority)
                .Include(x => x.IssueStatus)
                .Include(x => x.IssueType)
                .FirstOrDefaultAsync(i => i.IssueKey == key);

            if (issue == null)
            {
                return NotFound();
            }

            var issueDto = _mapper.Map<IssueDto>(issue);
            return Ok(issueDto);
        }

        [HttpPost]
        public async Task<ActionResult<IssueDto>> CreateIssue(CreateIssueDto createIssueDto)
        {
            var issue = _mapper.Map<Issue>(createIssueDto);
            _context.Issues.Add(issue);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue");
            }
            return CreatedAtAction(nameof(GetIssueById), new { id = issue.Id }, _mapper.Map<IssueDto>(issue));
        }

    }
}