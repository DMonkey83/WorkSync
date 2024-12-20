using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Entities
{
    [Table("IssueStatuses")]
    public class IssueStatus
    {
        [Key]
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        
        // Navigation properties
        public List<Issue> Issues { get; set; } = new List<Issue>();
        
    }
}