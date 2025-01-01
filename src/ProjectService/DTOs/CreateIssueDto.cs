namespace ProjectService.DTOs
{
    public class CreateIssueDto
    {
        public Guid ProjectId { get; set; }
        public Guid IssueTypeId { get; set; }
        public Guid? ReporterId { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid? PriorityId { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? ComponentId { get; set; }
        public Guid? SprintId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int? OriginalEstimate { get; set; }
        public int? RemainingEstimate { get; set; }
        public int? TimeSpent { get; set; }
    }
}