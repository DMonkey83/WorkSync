using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/issue-comments")]
    public class IssueCommentController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IssueCommentController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueCommentDto>>> GetAllIssueComments()
        {
            var issueComments = await _context.IssueComments
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
            return _mapper.Map<List<IssueCommentDto>>(issueComments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueCommentDto>> GetIssueCommentById(Guid id)
        {
            var issueComment = await _context.IssueComments
                .FirstOrDefaultAsync(c => c.Id == id);
            if (issueComment == null)
            {
                return NotFound();
            }
            var issueCommentDto = _mapper.Map<IssueCommentDto>(issueComment);
            return Ok(issueCommentDto);
        }

        [HttpPost]
        public async Task<ActionResult<IssueCommentDto>> CreateIssueComment(CreateIssueCommentDto createIssueCommentDto)
        {
            var issueComment = _mapper.Map<IssueComment>(createIssueCommentDto);
            _context.IssueComments.Add(issueComment);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue comment");
            }
            return CreatedAtAction(nameof(GetIssueCommentById), new { id = issueComment.Id }, _mapper.Map<IssueCommentDto>(issueComment));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssueCommentDto>> UpdateIssueComment(Guid id, UpdateIssueCommentDto updateIssueCommentDto)
        {
            var issueComment = await _context.IssueComments.FindAsync(id);
            if (issueComment == null)
            {
                return NotFound();
            }
            _mapper.Map(updateIssueCommentDto, issueComment);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue comment");
            }
            return _mapper.Map<IssueCommentDto>(issueComment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIssueComment(Guid id)
        {
            var issueComment = await _context.IssueComments.FindAsync(id);
            if (issueComment == null)
            {
                return NotFound();
            }
            _context.IssueComments.Remove(issueComment);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete issue comment");
            }
            return Ok();
        }
    }
}