using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueCustomFieldController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IssueCustomFieldController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueCustomFieldDto>> GetIssueCustomFieldById(Guid id)
        {
            var issueCustomField = await _context.IssueCustomFields.FindAsync(id);
            if (issueCustomField == null)
            {
                return NotFound();
            }
            return _mapper.Map<IssueCustomFieldDto>(issueCustomField);
        }

        [HttpPost]
        public async Task<ActionResult<IssueCustomFieldDto>> CreateIssueCustomField(CreateIssueCustomFieldDto createIssueCustomFieldDto)
        {
            var issueCustomField = _mapper.Map<IssueCustomField>(createIssueCustomFieldDto);
            _context.IssueCustomFields.Add(issueCustomField);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create issue custom field");
            }
            return CreatedAtAction(nameof(GetIssueCustomFieldById), new { id = issueCustomField.Id }, _mapper.Map<IssueCustomFieldDto>(issueCustomField));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IssueCustomFieldDto>> UpdateIssueCustomField(Guid id, UpdateIssueCustomFieldDto updateIssueCustomFieldDto)
        {
            var issueCustomField = await _context.IssueCustomFields.FindAsync(id);
            if (issueCustomField == null)
            {
                return NotFound();
            }
            _mapper.Map(updateIssueCustomFieldDto, issueCustomField);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update issue custom field");
            }
            return _mapper.Map<IssueCustomFieldDto>(issueCustomField);
        }   

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIssueCustomField(Guid id)
        {
            var issueCustomField = await _context.IssueCustomFields.FindAsync(id);
            if (issueCustomField == null)
            {
                return NotFound();
            }
            _context.IssueCustomFields.Remove(issueCustomField);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete issue custom field");
            }
            return Ok();
        }   
        
    }
}