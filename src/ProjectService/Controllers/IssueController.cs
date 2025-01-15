using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        [HttpGet("subissues/{id}")]
        public async Task<ActionResult<List<IssueDto>>> GetAllSubIssues(Guid id)
        {
            var issues = await _context.Issues
                .Include(x => x.IssuePriority)
                .Include(x => x.IssueStatus)
                .Include(x => x.Component)
                .Include(x => x.Sprint)
                .Include(x => x.Project)
                .Include(x => x.IssueType)
                .Where(i => i.ParentIssueId == id)  // Use Where instead of AnyAsync
                .ToListAsync();  // Convert to a list of issues
            return _mapper.Map<List<IssueDto>>(issues);  // Map to a list of IssueDto
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueDto>> GetIssueById(Guid id)
        {
            var issue = await _context.Issues
                .Include(x => x.IssuePriority)
                .Include(x => x.IssueStatus)
                .Include(x => x.IssueType)
                .Include(x => x.IssueComments)
                .Include(x => x.IssueLabels)
                .Include(x => x.IssueCustomFields)
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
                .Include(x => x.IssueComments)
                .Include(x => x.IssueCustomFields)
                .FirstOrDefaultAsync(i => i.IssueKey == key);

            if (issue == null)
            {
                return NotFound();
            }

            var issueDto = _mapper.Map<IssueDto>(issue);
            return Ok(issueDto);
        }

        [HttpPost]
        public async Task<ActionResult<IssueDto>> CreateIssue([FromBody] CreateIssueDto createIssueDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var issue = _mapper.Map<Issue>(createIssueDto);
            issue.Id = Guid.NewGuid();
            issue.CreatedAt = DateTime.UtcNow;
            issue.UpdatedAt = DateTime.UtcNow;
            _context.Issues.Add(issue);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue");
            }
            var createdIssue = await _context.Issues
                .Include(x => x.IssuePriority)
                .Include(x => x.IssueStatus)
                .Include(x => x.IssueType)
                .FirstOrDefaultAsync(i => i.Id == issue.Id);
            return CreatedAtAction(nameof(GetIssueById), new { id = issue.Id }, _mapper.Map<IssueDto>(createdIssue));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssueDto>> UpdateIssue(Guid id, UpdateIssueDto updateIssueDto)
        {
            var issue = await _context.Issues
                .Include(x => x.IssuePriority)
                .Include(x => x.IssueStatus)
                .Include(x => x.IssueType)
                .Include(x => x.IssueComments)
                .Include(x => x.IssueCustomFields)
                .Include(x => x.Component)
                .Include(x => x.Sprint)
                .Include(x => x.Project)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            issue.ComponentId = updateIssueDto.ComponentId ?? issue.ComponentId;
            issue.Description = updateIssueDto.Description ?? issue.Description;
            issue.PriorityId = updateIssueDto.PriorityId ?? issue.PriorityId;
            issue.ProjectId = updateIssueDto.ProjectId ?? issue.ProjectId;
            issue.SprintId = updateIssueDto.SprintId ?? issue.SprintId;
            issue.StatusId = updateIssueDto.StatusId ?? issue.StatusId;
            issue.Summary = updateIssueDto.Summary ?? issue.Summary;
            issue.IssueTypeId = updateIssueDto.IssueTypeId ?? issue.IssueTypeId;
            issue.UpdatedAt = DateTime.UtcNow;
            issue.TimeSpent = updateIssueDto.TimeSpent ?? issue.TimeSpent;
            issue.OriginalEstimate = updateIssueDto.OriginalEstimate ?? issue.OriginalEstimate;
            issue.RemainingEstimate = updateIssueDto.RemainingEstimate ?? issue.RemainingEstimate;
            issue.DueDate = updateIssueDto.DueDate ?? issue.DueDate;


            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue");
            }
            return Ok(_mapper.Map<IssueDto>(issue));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIssue(Guid id)
        {
            var issue = await _context.Issues
                .FirstOrDefaultAsync(i => i.Id == id);
            if (issue == null)
            {
                return NotFound();
            }
            _context.Issues.Remove(issue);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete issue");
            }
            return Ok("Issue deleted $issue.IssueKey");
        }

    }
}