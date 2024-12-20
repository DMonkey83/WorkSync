
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Entities
{
    [Table("Sprints")]
    public class Sprint
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public string SprintName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SprintStatus Status { get; set; } = SprintStatus.NotStarted;
        public string Goal { get; set; } = string.Empty;

        // Navigation properties
        public List<Issue> Issues { get; set; } = [];
        public Project Project { get; set; } = null;
    }
}