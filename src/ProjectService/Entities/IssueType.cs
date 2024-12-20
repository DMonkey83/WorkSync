using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Entities
{
    [Table("IssueTypes")]
    public class IssueType
    {
        [Key]
        public Guid Id { get; set; }
        public string IssueTypeName { get; set; }
        public string Description { get; set; } = String.Empty;

        // Navigation properties
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}