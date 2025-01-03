using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/components")]
    public class ComponentController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public ComponentController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComponentDto>>> GetAllComponents()
        {
            var components = await _context.Components
                .OrderBy(c => c.ComponentName)
                .ToListAsync();
            return _mapper.Map<List<ComponentDto>>(components);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDto>> GetComponentById(Guid id)
        {
            var component = await _context.Components
                .FirstOrDefaultAsync(c => c.Id == id);
            if (component == null)
            {
                return NotFound();
            }
            var componentDto = _mapper.Map<ComponentDto>(component);
            return Ok(componentDto);
        }

        [HttpPost]
        public async Task<ActionResult<ComponentDto>> CreateComponent(CreateComponentDto createComponentDto)
        {
            var component = _mapper.Map<Component>(createComponentDto);
            _context.Components.Add(component);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create component");
            }
            return CreatedAtAction(nameof(GetComponentById), new { id = component.Id }, _mapper.Map<ComponentDto>(component));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ComponentDto>> UpdateComponent(Guid id, UpdateComponentDto updateComponentDto)
        {
            var component = await _context.Components
                .FirstOrDefaultAsync(c => c.Id == id);
            if (component == null)
            {
                return NotFound();
            }
            component.ComponentName = updateComponentDto.ComponentName ?? component.ComponentName;
            component.Description = updateComponentDto.Description ?? component.Description;
            component.ProjectId = updateComponentDto.ProjectId ?? component.ProjectId;
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update component");
            }
            return Ok(_mapper.Map<ComponentDto>(component));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComponent(Guid id)
        {
            var component = await _context.Components
                .FirstOrDefaultAsync(c => c.Id == id);
            if (component == null)
            {
                return NotFound();
            }
            _context.Components.Remove(component);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete component");
            }
            return NoContent();
        }
    }
}