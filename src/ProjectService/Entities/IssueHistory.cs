using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Entities
{
    [Table("IssueHistories")]
    public class IssueHistory
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Issue))]
        public Guid IssueId { get; set; }
        public Guid ChangedBy { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedAt { get; set; }

        // Navigation properties
        public Issue Issue { get; set; } = null!;
    }
}