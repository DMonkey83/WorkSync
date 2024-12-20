using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Entities
{
    [Table("IssueLabels")]
    public class IssueLabel
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Issue))]
        public Guid IssueId { get; set; }
        public string LabelName { get; set; }

        // Navigation properties
        public Issue Issue { get; set; } = null!;
        
    }
}