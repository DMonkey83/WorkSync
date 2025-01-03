using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/issue-histories")]
    public class IssueHistoryController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IssueHistoryController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueHistoryDto>> GetIssueHistoryById(Guid id)
        {
            var issueHistory = await _context.IssueHistories.FindAsync(id);
            if (issueHistory == null)
            {
                return NotFound();
            }
            return _mapper.Map<IssueHistoryDto>(issueHistory);
        }

        [HttpPost]
        public async Task<ActionResult<IssueHistoryDto>> CreateIssueHistory(CreateIssueHistoryDto createIssueHistoryDto)
        {
            var issueHistory = _mapper.Map<IssueHistory>(createIssueHistoryDto);
            _context.IssueHistories.Add(issueHistory);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue history");
            }
            return CreatedAtAction(nameof(GetIssueHistoryById), new { id = issueHistory.Id }, _mapper.Map<IssueHistoryDto>(issueHistory));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssueHistoryDto>> UpdateIssueHistory(Guid id, UpdateIssueHistoryDto updateIssueHistoryDto)
        {
            var issueHistory = await _context.IssueHistories.FindAsync(id);
            if (issueHistory == null)
            {
                return NotFound();
            }

            issueHistory.NewValue = updateIssueHistoryDto.NewValue ?? issueHistory.NewValue;
            issueHistory.OldValue = updateIssueHistoryDto.OldValue ?? issueHistory.OldValue;

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue history");
            }
            return _mapper.Map<IssueHistoryDto>(issueHistory);
        }

    }
}