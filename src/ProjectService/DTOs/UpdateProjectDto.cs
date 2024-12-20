using System.ComponentModel.DataAnnotations;

namespace ProjectService.DTOs
{
    public class UpdateProjectDto
    {
        
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public Guid? LeadUserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
    }
}