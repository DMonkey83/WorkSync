using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ProjectDbContext context, IMapper mapper, ILogger<ProjectController> logger)
        {
            _logger = logger;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectDto createProjectDto)
        {
            try
            {
                var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
                _logger.LogDebug("User claims: {Claims}", string.Join(", ", claims));

                // Retrieve nameidentifier claim
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out var leadUserId))
                {
                    _logger.LogWarning("Invalid or missing nameidentifier claim. Found claims: {Claims}", string.Join(", ", claims));
                    return Unauthorized("Invalid user ID in token.");
                }

                var project = _mapper.Map<Project>(createProjectDto);
                project.Status = ProjectStatus.NotStarted;
                Console.WriteLine("LeadUserId: " + leadUserId);
                project.LeadUserId = leadUserId;
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

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            Console.WriteLine(id);
            if (project == null)
            {
                return NotFound();
            }
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            _logger.LogDebug("User claims: {Claims}", string.Join(", ", claims));

            // Retrieve nameidentifier claim
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                _logger.LogWarning("Invalid or missing nameidentifier claim. Found claims: {Claims}", string.Join(", ", claims));
                return Unauthorized("Invalid user ID in token.");
            }
          
            if (project.LeadUserId != userId) return Forbid("You are not authorized to update this project.");
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(Guid id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
              var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            _logger.LogDebug("User claims: {Claims}", string.Join(", ", claims));

            // Retrieve nameidentifier claim
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                _logger.LogWarning("Invalid or missing nameidentifier claim. Found claims: {Claims}", string.Join(", ", claims));
                return Unauthorized("Invalid user ID in token.");
            }
            if (project.LeadUserId != userId) return Forbid("You are not authorized to delete this project.");
            _context.Projects.Remove(project);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete project");
            }

            return Ok();
        }

        private static bool IsUniqueConstraintViolation(DbUpdateException ex)
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