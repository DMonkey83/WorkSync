using System.ComponentModel.DataAnnotations;

namespace ProjectService.DTOs
{
    public class CreateProjectDto
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectKey { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid LeadUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}