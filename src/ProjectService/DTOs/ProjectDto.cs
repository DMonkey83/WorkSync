
namespace ProjectService.DTOs
{
    public class ProjectDto
    {

        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectKey { get; set; }
        public string Description { get; set; }
        public Guid LeadUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public List<ComponentDto> Components { get; set; } = new List<ComponentDto>();

    }
}