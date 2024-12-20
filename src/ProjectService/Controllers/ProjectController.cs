using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public ProjectController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjects()
        {
            var projects = await _context.Projects
            .OrderBy(p => p.ProjectName)
            .ToListAsync();
            return _mapper.Map<List<ProjectDto>>(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(Guid id)
        {
            var project = await _context.Projects
            .Include(x => x.Components)
            .FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            var projectDto = _mapper.Map<ProjectDto>(project);
            return Ok(projectDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectDto createProjectDto)
        {
            try{
            var project = _mapper.Map<Project>(createProjectDto);
            project.Status = ProjectStatus.NotStarted;
            _context.Projects.Add(project);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create project");
            }
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, _mapper.Map<ProjectDto>(project));

            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                return BadRequest("Project with the same key already exists");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            Console.WriteLine(id);
            if (project == null)
            {
                return NotFound();
            }
            // TODO: check lead user id
            project.ProjectName = updateProjectDto.ProjectName ?? project.ProjectName;
            project.Description = updateProjectDto.Description ?? project.Description;
            project.StartDate = updateProjectDto.StartDate ?? project.StartDate;
            project.EndDate = updateProjectDto.EndDate ?? project.EndDate;
            project.Status = Enum.Parse<ProjectStatus>(updateProjectDto.Status ?? project.Status.ToString());

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update project");
            }

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(Guid id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete project");
            }

            return Ok();
        }

          private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            if (ex.InnerException is Npgsql.PostgresException postgresException) // For PostgreSQL
            {
                return postgresException.SqlState == "23505"; // Unique constraint violation error code
            }
            // Add additional checks for other databases (SQL Server, MySQL, etc.) if needed
            return false;
        }
    }
}