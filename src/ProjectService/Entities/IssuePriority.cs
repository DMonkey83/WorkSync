using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Entities
{
    [Table("IssuePriorities")]
    public class IssuePriority
    {
        [Key]
        public Guid Id { get; set; }
        public string PriorityName { get; set; } 

        // Navigation properties
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}