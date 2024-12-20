using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Entities
{
    [Table("Components")]
    public class Component
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public string ComponentName { get; set; }
        public string Description { get; set; } = String.Empty;
        public Guid LeadUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Project Project { get; set; }
        public List<Issue> Issues { get; set; } = [];
    }
}