namespace Contracts
{
    public class IssueUpdated
    {
        public Guid Id { get; set; }
        public Guid? ParentIssueId { get; set; }
        public Guid? ProjectId { get; set; }
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