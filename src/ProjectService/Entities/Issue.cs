using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Entities
{
    [Table("Issues")]
    public class Issue
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        [ForeignKey(nameof(Issue))]
        public Guid? ParentIssueId { get; set; }
        [ForeignKey(nameof(IssueType))]
        public Guid IssueTypeId { get; set; }
        public Guid? ReporterId { get; set; }
        public Guid? AssigneeId { get; set; }
        [ForeignKey(nameof(IssuePriority))]
        public Guid? PriorityId { get; set; }
        [ForeignKey(nameof(IssueStatus))]
        public Guid StatusId { get; set; }
        [ForeignKey(nameof(Component))]
        public Guid ComponentId { get; set; }
        [ForeignKey(nameof(Sprint))]
        public Guid SprintId { get; set; }
        public string IssueKey { get; set; }
        public string Summary { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public int OriginalEstimate { get; set; }
        public int RemainingEstimate { get; set; }
        public int TimeSpent { get; set; }
        
        // Navigation properties
        public Project Project { get; set; }
        public IssueType IssueType { get; set; }
        public Component Component { get; set; }
        public Sprint Sprint { get; set; }
        public IssuePriority IssuePriority { get; set; }
        public IssueStatus IssueStatus { get; set; }
        public Issue ParentIssue { get; set; }
        public List<IssueLabel> IssueLabels { get; set; } = new List<IssueLabel>();
        public List<IssueComment> IssueComments { get; set; } = new List<IssueComment>();
        public ICollection<IssueCustomField> IssueCustomFields { get; set; }
    }
}