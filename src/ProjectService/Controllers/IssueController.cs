using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public IssueController(ProjectDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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
            var newIssue = _mapper.Map<IssueDto>(issue);

            Console.WriteLine("Publishing IssueCreated event issue Id: ", newIssue.Id);

            await _publishEndpoint.Publish(_mapper.Map<IssueCreated>(newIssue));
            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return BadRequest("Could not create issue");
            }

            var createdIssue = await _context.Issues
                .Include(x => x.IssuePriority)
                .Include(x => x.IssueStatus)
                .Include(x => x.IssueType)
                .FirstOrDefaultAsync(i => i.Id == newIssue.Id);
            return CreatedAtAction(nameof(GetIssueById), new { id = issue.Id }, _mapper.Map<IssueDto>(createdIssue));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssueDto>> UpdateIssue(Guid id, UpdateIssueDto updateIssueDto)
        {
            // Fetch the issue, including related entities
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

            // Update properties with values from the DTO, if present
            issue.ComponentId = updateIssueDto.ComponentId ?? issue.ComponentId;
            issue.Description = updateIssueDto.Description ?? issue.Description;
            issue.PriorityId = updateIssueDto.PriorityId ?? issue.PriorityId;
            issue.ProjectId = updateIssueDto.ProjectId ?? issue.ProjectId;
            issue.SprintId = updateIssueDto.SprintId ?? issue.SprintId;
            issue.StatusId = updateIssueDto.StatusId ?? issue.StatusId;
            issue.Summary = updateIssueDto.Summary ?? issue.Summary;
            issue.IssueTypeId = updateIssueDto.IssueTypeId ?? issue.IssueTypeId;
            issue.TimeSpent = updateIssueDto.TimeSpent ?? issue.TimeSpent;
            issue.OriginalEstimate = updateIssueDto.OriginalEstimate ?? issue.OriginalEstimate;
            issue.RemainingEstimate = updateIssueDto.RemainingEstimate ?? issue.RemainingEstimate;
            issue.DueDate = updateIssueDto.DueDate ?? issue.DueDate;

            // Update the last modified timestamp
            issue.UpdatedAt = DateTime.UtcNow;

            // Save changes to the database
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue");
            }

            // Validate IssueKey before publishing
            Console.WriteLine($"Publishing IssueUpdated event. IssueKey: {issue.IssueKey}, Id: {issue.Id}");
            var updatedIssue = new UpdateIssueDto
            {
                IssueKey = issue.IssueKey,
                Id = issue.Id,
                ProjectId = issue.ProjectId,
                IssueTypeId = issue.IssueTypeId,
                ReporterId = issue.ReporterId,
                AssigneeId = issue.AssigneeId,
                PriorityId = issue.PriorityId,
                StatusId = issue.StatusId,
                ComponentId = issue.ComponentId,
                SprintId = issue.SprintId,
                Summary = issue.Summary,
                Description = issue.Description,
                DueDate = issue.DueDate,
                OriginalEstimate = issue.OriginalEstimate,
                RemainingEstimate = issue.RemainingEstimate,
                TimeSpent = issue.TimeSpent,
                UpdatedAt = issue.UpdatedAt
            };
            // Publish the IssueUpdated event
            await _publishEndpoint.Publish<IssueUpdated>(updatedIssue);

            // Return the updated issue DTO
            return Ok(_mapper.Map<IssueDto>(issue));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIssue(Guid id)
        {
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issue);

            // Create the IssueDeleted event object
            var issueDeleted = new IssueDeleted
            {
                Id = issue.Id.ToString() // Assuming IssueDeleted.Id is a string
            };

            // Publish the event
            await _publishEndpoint.Publish(issueDeleted);

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete issue");
            }

            return Ok($"Issue deleted {issue.IssueKey}");
        }

    }
}