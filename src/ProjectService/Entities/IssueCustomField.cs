using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Entities
{
    [Table("IssueCustomFields")]
    public class IssueCustomField
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Issue))]
        public Guid IssueId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

        // Navigation properties
        public Issue Issue { get; set; } = null!;
        
    }
}