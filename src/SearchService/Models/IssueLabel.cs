namespace SearchService.Models
{
    public class IssueLabel
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string LabelName { get; set; }
    }
}