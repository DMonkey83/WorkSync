using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/issue-labels")]
    public class IssueLabelController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IssueLabelController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueLabelDto>>> GetAllIssueLabels()
        {
            var issueLabels = await _context.IssueLabels.ToListAsync();
            return _mapper.Map<List<IssueLabelDto>>(issueLabels);
        }        

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueLabelDto>> GetIssueLabelById(Guid id)
        {
            var issueLabel = await _context.IssueLabels.FindAsync(id);
            if (issueLabel == null)
            {
                return NotFound();
            }
            return _mapper.Map<IssueLabelDto>(issueLabel);
        }

        [HttpPost]
        public async Task<ActionResult<IssueLabelDto>> CreateIssueLabel(CreateIssueLabelDto createIssueLabelDto)
        {
            var issueLabel = _mapper.Map<IssueLabel>(createIssueLabelDto);
            _context.IssueLabels.Add(issueLabel);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue label");
            }
            return CreatedAtAction(nameof(GetIssueLabelById), new { id = issueLabel.Id }, _mapper.Map<IssueLabelDto>(issueLabel));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssueLabelDto>> UpdateIssueLabel(Guid id, UpdateIssueLabelDto updateIssueLabelDto)
        {
            var issueLabel = await _context.IssueLabels.FindAsync(id);
            if (issueLabel == null)
            {
                return NotFound();
            }

            issueLabel.LabelName = updateIssueLabelDto.LabelName ?? issueLabel.LabelName;

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue label");
            }
            return _mapper.Map<IssueLabelDto>(issueLabel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIssueLabel(Guid id)
        {
            var issueLabel = await _context.IssueLabels.FindAsync(id);
            if (issueLabel == null)
            {
                return NotFound();
            }

            _context.IssueLabels.Remove(issueLabel);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete issue label");
            }
            return NoContent();
        }
    }
}