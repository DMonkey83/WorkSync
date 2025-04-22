using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace SearchService.Models
{
    public class Issue : Entity
    {

        public string ParentIssueId { get; set; }
        public string ProjectId { get; set; }
        public string IssueKey { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public int OriginalEstimate { get; set; }
        public int RemainingEstimate { get; set; }
        public int TimeSpent { get; set; }
        public string IssuePriorityName { get; set; }
        public string IssueStatusName { get; set; }
        public string IssueTypeName { get; set; }
        public List<IssueLabel> IssueLabels { get; set; }
    }
}