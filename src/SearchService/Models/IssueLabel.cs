namespace SearchService.Models
{
    public class IssueLabel
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid LabelName { get; set; }
    }
}