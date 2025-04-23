using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectService.Entities
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectKey { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid LeadUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.InProgress;

        //Navigation properties
        public ICollection<Component> Components { get; set; } = new HashSet<Component>();
    }
}