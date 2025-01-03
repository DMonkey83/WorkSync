using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/issue-statuses")]
    public class IssueStatusController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IssueStatusController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueStatusDto>>> GetAllIssueStatuses()
        {
            var issueStatuses = await _context.IssueStatuses.ToListAsync();
            return _mapper.Map<List<IssueStatusDto>>(issueStatuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueStatusDto>> GetIssueStatusById(Guid id)
        {
            var issueStatus = await _context
            .IssueStatuses
            .Include(x => x.Issues)
            .FirstOrDefaultAsync(i => i.Id == id);
            if (issueStatus == null)
            {
                return NotFound();
            }
            return _mapper.Map<IssueStatusDto>(issueStatus);
        }

        [HttpPost]
        public async Task<ActionResult<IssueStatusDto>> CreateIssueStatus(CreateIssueStatusDto createIssueStatusDto)
        {
            var issueStatus = _mapper.Map<IssueStatus>(createIssueStatusDto);
            _context.IssueStatuses.Add(issueStatus);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue status");
            }
            return CreatedAtAction(nameof(GetIssueStatusById), new { id = issueStatus.Id }, _mapper.Map<IssueStatusDto>(issueStatus));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssueStatusDto>> UpdateIssueStatus(Guid id, UpdateIssueStatusDto updateIssueStatusDto)
        {
            var issueStatus = await _context.IssueStatuses.FindAsync(id);
            if (issueStatus == null)
            {
                return NotFound();
            }

            issueStatus.StatusName = updateIssueStatusDto.StatusName ?? issueStatus.StatusName;

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue status");
            }
            return _mapper.Map<IssueStatusDto>(issueStatus);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIssueStatus(Guid id)
        {
            var issueStatus = await _context.IssueStatuses.FindAsync(id);
            if (issueStatus == null)
            {
                return NotFound();
            }
            _context.IssueStatuses.Remove(issueStatus);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete issue status");
            }
            return Ok();
        }
    }
}