namespace ProjectService.DTOs
{
    public class IssueLabelDto
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string LabelName { get; set; }
    }
}